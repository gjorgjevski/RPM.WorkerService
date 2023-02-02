using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM.WorkerService.Models
{
    public class Series
    {
        public string Series_id { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
        public string F { get; set; }
        public string Unitsshort { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string Source { get; set; }
        public string Iso3166 { get; set; }
        public string Geography { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public DateTime Updated { get; set; }
        public List<List<object>> Data { get; set; }
    }
}
