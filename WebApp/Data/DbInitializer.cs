using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(BooksDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }

            var publishers = new Publisher[]
            {
                new Publisher{ Name="First", Country="UK" },
                new Publisher{ Name="Second", Country="Germany" },
                new Publisher{ Name="Third", Country="USA" },
            };
            foreach (Publisher p in publishers)
            {
                context.Publishers.Add(p);
            }
            context.SaveChanges();

            var books = new Book[]
            {
                new Book{ Name="Java For Dummies", Year=2012, PublisherId=1 },
                new Book{ Name="C# For Dummies", Year=2010, PublisherId=1 },
                new Book{ Name="Python For Dummies", Year=2014, PublisherId=1 },
                new Book{ Name="Javascript For Dummies", Year=2010, PublisherId=2 },
                new Book{ Name="C++ For Dummies", Year=2005, PublisherId=3 },
                new Book{ Name="PHP For Dummies", Year=1998, PublisherId=2 }
            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();            
        }
    }
}
