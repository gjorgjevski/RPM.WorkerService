using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Models
{
    public class Prices
    {
        public int Id { get; set; }
        public DateTime DateOfPrice { get; set; }
        public Double Price { get; set; }
    }
}
