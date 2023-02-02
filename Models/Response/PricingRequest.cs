using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Models
{
    public class PricingRequest
    {
        public RequestCommand Request { get; set; }
        public List<Series> Series { get; set; }
    }
}
