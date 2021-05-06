using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BooksAndPublishersViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}
