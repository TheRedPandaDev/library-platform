using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BooksStats
    {
        public int Count { get; set; }
        public Dictionary<int, int> CountByYear { get; set; }
        public int MinYear { get; set; }
    }
}
