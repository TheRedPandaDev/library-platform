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
            ViewData["Title"] = "Книги";
            var books = _booksDBContext.Books.Include(b => b.Publisher).ToList();

            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Book book = new Book();
            var publishers = _booksDBContext.Publishers.ToList();
            var bookPublishersViewModel = new BookPublishersViewModel
            {
                Book = book,
                Publishers = publishers
            };

            return View(bookPublishersViewModel);
        }

        [HttpPost]
        public IActionResult Create(BookPublishersViewModel bookPublishersViewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = bookPublishersViewModel.Book;
                if (book.Id != 0)
                {
                    _booksDBContext.Entry(book).State = EntityState.Modified;
                } else
                {
                    _booksDBContext.Books.Add(bookPublishersViewModel.Book);
                }                
                _booksDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Edit(Book book)
        {
            var publishers = _booksDBContext.Publishers.ToList();
            var bookPublishersViewModel = new BookPublishersViewModel
            {
                Book = book,
                Publishers = publishers
            };

            return View("Create", bookPublishersViewModel);
        }

        public IActionResult Delete(Book book)
        {
            _booksDBContext.Books.Remove(book);
            _booksDBContext.SaveChanges();
            return RedirectToAction("Index");
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
