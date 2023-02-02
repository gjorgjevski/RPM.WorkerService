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
        public PricingContext(DbContextOptions options) : base(options) { } // calls the base contructor with options

        public DbSet<Pricing> Pricing { get; set; }
        
    }
}
