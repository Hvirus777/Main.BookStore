using Main.BookStore.Models;
using Main.BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [Route("[controller]/[action]")]
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

        [Route("~/all-books")]

        public async Task<IActionResult> GetAllBooks()
        {
            PageTitle = "Books";

            var books = await _bookRepository.GetAllBooks();
            return View(books);
        }


        /*
          Route constraints eg. should attach type of parameters with :type of parameters and there are many more constraints. So if the value doesnt match the parameter constraints then it will return 404 page not found and this can be handled by Exception handling and we can show nice decorated 404 page not found page. 

        few eg of RouteContraints 
         [Route("/book-details/{name:alpha:minLength(5):regex()}"]

        https://github.com/dotnet/aspnetcore/blob/main/src/Http/Routing/src/Constraints/DecimalRouteConstraint.cs
        
        */

        [Route("/book-details/{id:int:min(1)}", Name = "BookDetailsRoute")]
        public async Task<IActionResult> GetBook(int id, string nameOfBook)
        {

            // Passing data as anonymous to understand Dynamic View Concept. Avoid Dynamic view concept

            //dynamic data = new ExpandoObject();
            //data.book = await _bookRepository.GetBookById(id);
            //data.name = "Harsh Soni";

            var data = await _bookRepository.GetBookById(id);


            PageTitle = data.Title + " Book Details ";


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


            ViewBag.Language = new SelectList(await _languageRepository.GetAllLanguage(), "Id", "Name");

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
                    string folder = "books/cover/";

                    bookModel.CoverImageURL = await UploadImage(folder, bookModel.CoverPhoto);
                }

                if (bookModel.GalleryFile != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.GalleryFile)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        };

                        bookModel.Gallery.Add(gallery);

                    }
                }

                if (bookModel.BookPDF != null)
                {
                    string folder = "books/pdf/";

                    bookModel.BookPDFUrl = await UploadImage(folder, bookModel.BookPDF);
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

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

    }
}
