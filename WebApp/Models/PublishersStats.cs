using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PublishersStats
    {
        public int Count { get; set; }
        public Dictionary<string, int> CountByCountry { get; set; }
        public int AvgBooksCount { get; set; }
    }
}
