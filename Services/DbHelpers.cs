using Microsoft.EntityFrameworkCore;
using RPM.WorkerService.DataAccess;
using RPM.WorkerService.Models;
using RPM.WorkerService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Services
{
    public class DbHelpers
    {
        private PricingContext _dbContext;

        private DbContextOptions<PricingContext> GetAllOptions(WorkerOptions options)
        {
            DbContextOptionsBuilder<PricingContext> optionsBuilder = new DbContextOptionsBuilder<PricingContext>();

            optionsBuilder.UseSqlServer(options.ConnectionString);

            return optionsBuilder.Options;
        }

        public async Task AddPrices(List<Prices> prices, WorkerOptions options)
        {
            using (_dbContext = new PricingContext(GetAllOptions(options)))
            {
                try
                {
                   await _dbContext.Prices.AddRangeAsync(prices);
                   _dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
