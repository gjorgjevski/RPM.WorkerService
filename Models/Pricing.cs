using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Models
{
    public class Prices
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateOfPrice { get; set; }
        [Required]
        public Double Price { get; set; }
    }
}
