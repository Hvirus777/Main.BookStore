using Main.BookStore.Models;
using Main.BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Dynamic;

namespace Main.BookStore.Controllers
{
    public class BookController : Controller
    {
        [ViewData]
        public string PageTitle { get; set; }

        private readonly BookRepository _bookRepository = null;


        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ViewResult GetAllBooks()
        {
            PageTitle = "Books";

            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        [Route("/book-details/{id}", Name = "BookDetailsRoute")]
        public ViewResult GetBook(int id, string nameOfBook)
        { // Passing data as anonymous to understand Dynamic View Concept. Avoid Dynamic view concept

            dynamic data = new ExpandoObject();
            data.book = _bookRepository.GetBookById(id);
            data.name = "Harsh Soni";


            // var book= 


            PageTitle = data.book.Title + " Book Details ";
            return View(data);

        }
        public List<BookModel> SearchBook(string title, string author)
        {
            return _bookRepository.SearchBook(title, author);
        }


        [Route("/New-Book")]
        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            PageTitle = " Add new Book";
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        [Route("/New-Book")]
        public IActionResult AddNewBook(BookModel bookModel)
        {

            var id = _bookRepository.AddNewBook(bookModel);

            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
            }

            return View();
        }
    }
}
