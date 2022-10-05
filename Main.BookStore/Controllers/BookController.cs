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

        public List<BookModel> GetAllBooks()
        {
           return _bookRepository.GetAllBooks();
        }
        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }
        public List<BookModel> SearchBook(string title, string author)
        {
            return _bookRepository.SearchBook(title,author);
        }
    }
}
