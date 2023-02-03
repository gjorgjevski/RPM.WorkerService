using Microsoft.EntityFrameworkCore;
using RPM.WorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.DataAccess
{
    public class PricingContext : DbContext
    {
        public PricingContext(DbContextOptions<PricingContext> options) : base(options){}
        public DbSet<Prices> Prices { get; set; }
       

    }
}
