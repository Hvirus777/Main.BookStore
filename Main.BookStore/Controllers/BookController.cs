using Main.BookStore.Models;
using Main.BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

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

        public async Task<IActionResult> GetAllBooks()
        {
            PageTitle = "Books";

            var books = await _bookRepository.GetAllBooks();
            return View(books);
        }

        [Route("/book-details/{id}", Name = "BookDetailsRoute")]
        public async Task<IActionResult> GetBook(int id, string nameOfBook)
        {

            // Passing data as anonymous to understand Dynamic View Concept. Avoid Dynamic view concept

            dynamic data = new ExpandoObject();
            data.book = await _bookRepository.GetBookById(id);
            data.name = "Harsh Soni";

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
            // if you want to pass selectList in view then just remove 'new selectList()' tag and just pas List<string>
            ViewBag.Language = new SelectList(new List<string>() { "English", "Hindi", "Dutch" });

            PageTitle = " Add new Book";
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        [Route("/New-Book")]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                var id = await _bookRepository.AddNewBook(bookModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }

            }
            ViewBag.Language = new SelectList(new List<string>() { "English", "Hindi", "Dutch" });
            ModelState.AddModelError("", "This is customer Error message 1");
            ModelState.AddModelError("", "This is customer Error message 2");

            return View();
        }
    }
}
