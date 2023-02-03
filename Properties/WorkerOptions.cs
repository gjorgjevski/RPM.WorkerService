using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Properties
{
    public class WorkerOptions
    {
        public int LastNumberOfDays { get; set; }
        public int DelayOfExecution { get; set; }
        public string DayOfWeek { get; set; }
        public string SeriesId { get; set; }
        public string PricesApiURL { get; set; }
        public string ConnectionString { get; set; }
    }
}
