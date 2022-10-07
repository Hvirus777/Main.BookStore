using Main.BookStore.Models;
using Main.BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
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
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel()
            {
                LanguageId = 2
            };


            ViewBag.Language =  new SelectList(await _languageRepository.GetAllLanguage(),"Id","Name");
            
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
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "/books/cover/";
                    folder += Guid.NewGuid().ToString() + bookModel.CoverPhoto.FileName;

                    bookModel.CoverImageURL = folder;

                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }


                var id = await _bookRepository.AddNewBook(bookModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }

            }
            ViewBag.Language = new SelectList(await _languageRepository.GetAllLanguage(), "Id", "Name");



            return View();
        }

       
    }
}
