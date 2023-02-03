using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RPM.WorkerService.DataAccess;
using RPM.WorkerService.Models;
using RPM.WorkerService.Properties;
using RPM.WorkerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RPM.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient _client;
        WorkerOptions _options;



        public Worker(ILogger<Worker> logger,WorkerOptions options)
        {
            _logger = logger;
            _options = options;
        }


        public override Task StartAsync(CancellationToken cancellationToken) {
            // create httpClient here only once on start
            _client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
              
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                DayOfWeek dayOfweekk = DateTime.Today.DayOfWeek;

                // check if it is the correct day of week and then execute
                if (dayOfweekk.ToString() == _options.DayOfWeek)
                {

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    HttpResponseMessage result = await _client.GetAsync(_options.PricesApiURL);

                    if (result.IsSuccessStatusCode)
                    {

                        List<Prices> newestPrices = new List<Prices>();
                        var response = await result.Content.ReadAsAsync<PricingResponse>();

                        //check by seriesId in case in future more series are added
                        var takeTheProperSeries = response.Series.Where(x => x.Series_id == _options.SeriesId).ToList();

                        // take last N prices
                        var prices = takeTheProperSeries.FirstOrDefault().Data.Take(_options.LastNumberOfDays);

                        try
                        {
                            foreach (var element in prices)
                            {
                                Prices temp = new Prices { Price = (double)element[1], DateOfPrice = DateTime.ParseExact(element[0].ToString(), "yyyyMMdd", null) };
                                newestPrices.Add(temp);
                            }
                            DbHelpers dbHelper = new DbHelpers(); 

                            await dbHelper.AddPrices(newestPrices, _options);

                            _logger.LogInformation("Prices successfully added at: {time}", DateTimeOffset.Now);

                            await Task.Delay(_options.DelayOfExecution, stoppingToken); // delay here so it will not continue in the same day
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Prices cannot be added. Exception:{ex} ", ex.InnerException.ToString());
                        }


                    }
                    else
                    {
                        // todo - here we can include sending and email to someone responsible that can check why the API is down
                        _logger.LogError("The API is down at: {time}", DateTimeOffset.Now);
                        await Task.Delay(3600000, stoppingToken); // delay 5 hours if the API is down
                    }
                }

                else {                  
                    // delay 24 hours and then continue 
                    await Task.Delay(_options.DelayOfExecution, stoppingToken);
                }                
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // dispose the client at the end
            _client.Dispose(); 
            
            return base.StopAsync(cancellationToken);
        }
    }
}
