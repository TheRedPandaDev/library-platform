using System;

namespace WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
