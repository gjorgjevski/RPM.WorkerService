using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Models
{
    public class PricingResponse
    {
        public CommandResponse Request { get; set; }
        public List<Series> Series { get; set; }
    }
}
