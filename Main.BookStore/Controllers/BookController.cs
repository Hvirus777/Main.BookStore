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
       

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public ViewResult GetAllBooks()
        {
            PageTitle = "Books";

            var books= _bookRepository.GetAllBooks();
            return View(books);
        }
        public ViewResult GetBook(int id)
        { // Passing data as anonymous to understand Dynamic View Concept. Avoid Dynamic view concept

            dynamic data = new ExpandoObject(); 
            data.book= _bookRepository.GetBookById(id);
            data.name = "Harsh Soni";


           // var book= 


            PageTitle = data.book.Title + " Book Details ";
            return View(data); 

        }
        public List<BookModel> SearchBook(string title, string author)
        {
            return _bookRepository.SearchBook(title,author);
        }
    }
}
