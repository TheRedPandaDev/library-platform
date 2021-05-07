using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class StatsViewModel
    {
        public BooksStats BooksStats { get; set; }
        public PublishersStats PublishersStats { get; set; }
    }
}
