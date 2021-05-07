using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BooksDBContext _booksDBContext;

        /*public HomeController(BooksDBContext booksDBContext)
        {
            _booksDBContext = booksDBContext;
        }*/

        public HomeController(ILogger<HomeController> logger, BooksDBContext booksDBContext)
        {
            _logger = logger;
            _booksDBContext = booksDBContext;
        }

        public IActionResult Index()
        {
            var booksAndPublishersViewModel = new BooksAndPublishersViewModel
            {
                Books = _booksDBContext.Books.Include(b => b.Publisher).ToList(),
                Publishers = _booksDBContext.Publishers.Include(b => b.PublishedBooks).ToList()
            };

            return View(booksAndPublishersViewModel);
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            Book book = new Book();
            var publishers = _booksDBContext.Publishers.ToList();
            var bookPublishersViewModel = new BookPublishersViewModel
            {
                Book = book,
                Publishers = publishers,
                IsEdit = false
            };

            return View("CreateBook", bookPublishersViewModel);
        }

        [HttpGet]
        public IActionResult CreatePublisher()
        {
            var publisher = new Publisher();
            var publisherViewModel = new PublisherViewModel
            {
                Publisher = publisher,
                IsEdit = false
            };

            return View("CreatePublisher", publisherViewModel);
        }

        [HttpPost]
        public IActionResult CreateBook(BookPublishersViewModel bookPublishersViewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = bookPublishersViewModel.Book;
                if (bookPublishersViewModel.IsEdit)
                {
                    _booksDBContext.Entry(book).State = EntityState.Modified;
                }
                else
                {
                    _booksDBContext.Books.Add(book);
                }                
                _booksDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult CreatePublisher(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                Publisher publisher = publisherViewModel.Publisher;
                if (publisherViewModel.IsEdit)
                {
                    _booksDBContext.Entry(publisher).State = EntityState.Modified;
                }
                else
                {
                    _booksDBContext.Publishers.Add(publisher);
                }
                _booksDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult EditBook(Book book)
        {
            var publishers = _booksDBContext.Publishers.ToList();
            var bookPublishersViewModel = new BookPublishersViewModel
            {
                Book = book,
                Publishers = publishers,
                IsEdit = true
            };

            return View("CreateBook", bookPublishersViewModel);
        }

        public IActionResult EditPublisher(Publisher publisher)
        {
            var publisherViewModel = new PublisherViewModel
            {
                Publisher = publisher,
                IsEdit = true
            };

            return View("CreatePublisher", publisherViewModel);
        }

        public IActionResult DeleteBook(Book book)
        {
            _booksDBContext.Books.Remove(book);
            _booksDBContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeletePublisher(Publisher publisher)
        {
            _booksDBContext.Publishers.Remove(publisher);
            _booksDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Stats()
        {
            var countByYear = _booksDBContext.Books.ToList().GroupBy(b => b.Year)
                .ToDictionary(g => g.Key, g => g.Count());

            var countByCountry = _booksDBContext.Publishers.ToList().GroupBy(p => p.Country)
                .ToDictionary(g => g.Key, g => g.Count());

            var publishedBooks = _booksDBContext.Publishers.ToDictionary(p => p.Id, p => p.PublishedBooks.Count);

            var statsViewModel = new StatsViewModel
            {
                BooksStats = new BooksStats
                {
                    Count = _booksDBContext.Books.Count(),
                    CountByYear = countByYear,
                    MinYear = _booksDBContext.Books.Select(b => b.Year).Min()
                },
                PublishersStats = new PublishersStats
                {
                    Count = _booksDBContext.Publishers.Count(),
                    CountByCountry = countByCountry,
                    AvgBooksCount = (int) publishedBooks.Values.Average()
                }
            };

            return View("Stats", statsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
