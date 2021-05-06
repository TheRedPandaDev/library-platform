using System.Collections.Generic;

namespace WebApp.Models
{
    public class Publisher
    {
        public Publisher() =>
            PublishedBooks = new HashSet<Book>();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Book> PublishedBooks { get; set; }
    }
}
