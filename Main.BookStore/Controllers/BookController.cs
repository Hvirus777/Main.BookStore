using Main.BookStore.Models;
using Main.BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Main.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

       

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public ViewResult GetAllBooks()
        {
            var books= _bookRepository.GetAllBooks();
            return View(books);
        }
        public ViewResult GetBook(int id)
        {
            var book= _bookRepository.GetBookById(id);
            return View(book); 
        }
        public List<BookModel> SearchBook(string title, string author)
        {
            return _bookRepository.SearchBook(title,author);
        }
    }
}
