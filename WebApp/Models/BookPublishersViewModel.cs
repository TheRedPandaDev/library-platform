using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BookPublishersViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public Boolean IsEdit { get; set; }
    }
}
