using Main.BookStore.Models;
using Main.BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            var model = new BookModel()
            {
                Language = "2"
            };



            //ViewBag.Language = LanguageList().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value = x.Id.ToString(),
            //    Disabled=x.Disable,
            //   // Group=x.Group
            //}).ToList();

            PageTitle = " Add new Book";
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
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
            //ViewBag.Language = new SelectList(LanguageList(), "Id", "Text");



            return View();
        }

        private List<LanguageModel> LanguageList()
        { 
            var group1 = new SelectListGroup { Name = "Group1" };
            var group2 = new SelectListGroup { Name = "Group2"};
            var group3 = new SelectListGroup { Name = "Group3", Disabled = true };

            return new List<LanguageModel>() {
                new LanguageModel() { Id=1,Text="English",Disable=false,Group=group1},
                new LanguageModel() { Id=2,Text="Hindi",Disable=false,Group=group1},
                new LanguageModel() { Id=3,Text="Dutch",Disable=false,Group=group2},
                new LanguageModel() { Id=4,Text="Japanese",Disable=false, Group = group2},
                new LanguageModel() { Id=5,Text="Korean",Disable=false, Group = group3},
                new LanguageModel() { Id=6,Text="Tamil",Disable=false, Group = group3}
            };
        }
    }
}
