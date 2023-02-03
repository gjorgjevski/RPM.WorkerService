using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RPM.WorkerService.DataAccess;
using RPM.WorkerService.Properties;
using Serilog;
using Serilog.Events;
using System;

namespace RPM.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // override the default microsoft logger so we can log into a file, using serilog logger bellow ( we would normaly put this configuration in appsettings.json )
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\temp\workerService\LogFile.txt")
                .CreateLogger();

            try {
                Log.Information("Starting up the service");
                CreateHostBuilder(args).Build().Run();
                return; // exit the app
            }
            catch (Exception ex) {
                Log.Fatal(ex,"There was a problem starting the service.");
            }
            finally {
                // ensures if there is any messages in buffer they get written before we close the app
                Log.CloseAndFlush(); 
            }

            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration; // inject configuration

                    services.AddDbContext<PricingContext>(options => {
                        options.UseSqlServer(configuration.GetConnectionString("Default"));
                    });
                    WorkerOptions options = configuration.GetSection("WorkerVariables").Get<WorkerOptions>();

                    services.AddSingleton(options);
                    services.AddHostedService<Worker>();
                })
                .UseSerilog();
    }
}
