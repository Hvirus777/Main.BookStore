using Main.BookStore.Data;
using Main.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Main.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {

        private readonly BookStoreContext _context = null;
        private readonly IConfiguration configuration;

        public BookRepository(BookStoreContext context,IConfiguration _configuration)
        {
            _context = context;
            configuration = _configuration;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBooks = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                LanguageId = model.LanguageId,
                CoverImageUrl = model.CoverImageURL,
                BookPdfUrl = model.BookPDFUrl

            };

            newBooks.bookGallery = new List<GalleryOfBooks>();

            foreach (var file in model.Gallery)
            {
                newBooks.bookGallery.Add(new GalleryOfBooks()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }


            await _context.books.AddAsync(newBooks);
            await _context.SaveChangesAsync();

            return newBooks.Id;

        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            #region CodeByMeUsingIncluce

            // var books = new List<BookModel>();
            //var allBooks = await _context.books.Include(x => x.Language).ToListAsync();

            //if (allBooks?.Any() == true)
            //{

            //    foreach (var book in allBooks)
            //    {
            //        var bookData = new BookModel();

            //        bookData.Author = book.Author;
            //        bookData.Title = book.Title;
            //        bookData.Description = book.Description;
            //        bookData.TotalPages = book.TotalPages;
            //        bookData.Category = book.Category;
            //        bookData.Id = book.Id;
            //        bookData.LanguageId = book.LanguageId;
            //        bookData.CreatedOn = book.CreatedOn;
            //        bookData.UpdatedOn = book.UpdatedOn;
            //        //  bookData.Language = book.Language.Name.Where(id == book.LanguageId)

            //        var language = new LanguageModel()
            //        {

            //        };

            //        books.Add(bookData);
            //    }
            #endregion

            var bookList = await _context.books.Select(book => new BookModel
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageURL = book.CoverImageUrl,
                Gallery = book.bookGallery.Select(x => new GalleryModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    URL = x.URL
                }).ToList()

            }).ToListAsync();

            return bookList;
        }


        public async Task<List<BookModel>> GetTopBookAsync(int count)
        {
            #region CodeByMeUsingIncluce

            // var books = new List<BookModel>();
            //var allBooks = await _context.books.Include(x => x.Language).ToListAsync();

            //if (allBooks?.Any() == true)
            //{

            //    foreach (var book in allBooks)
            //    {
            //        var bookData = new BookModel();

            //        bookData.Author = book.Author;
            //        bookData.Title = book.Title;
            //        bookData.Description = book.Description;
            //        bookData.TotalPages = book.TotalPages;
            //        bookData.Category = book.Category;
            //        bookData.Id = book.Id;
            //        bookData.LanguageId = book.LanguageId;
            //        bookData.CreatedOn = book.CreatedOn;
            //        bookData.UpdatedOn = book.UpdatedOn;
            //        //  bookData.Language = book.Language.Name.Where(id == book.LanguageId)

            //        var language = new LanguageModel()
            //        {

            //        };

            //        books.Add(bookData);
            //    }
            #endregion

            var bookList = await _context.books.Select(book => new BookModel
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageURL = book.CoverImageUrl,
                Gallery = book.bookGallery.Select(x => new GalleryModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    URL = x.URL
                }).ToList()

            }).Take(count).ToListAsync();

            return bookList;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            #region Code By me
            /* Using 'Include' Functionality of Linq i am geting language data into book data as well*/


            //var book = await _context.books.Include(x => x.Language).Where(x => x.Id == id).FirstOrDefaultAsync();

            //if (book != null)
            //{
            //    var bookData = new BookModel();

            //    bookData.Author = book.Author;
            //    bookData.Title = book.Title;
            //    bookData.Description = book.Description;
            //    bookData.TotalPages = book.TotalPages;
            //    bookData.Category = book.Category;
            //    bookData.Id = book.Id;
            //    bookData.LanguageId = book.LanguageId;
            //    bookData.Language = book.Language.Name;
            //    bookData.CreatedOn = book.CreatedOn;
            //    bookData.UpdatedOn = book.UpdatedOn;

            //    return bookData;
            //}
            #endregion 

            var bookData = await _context.books.Where(x => x.Id == id)
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     LanguageId = book.LanguageId,
                     Language = book.Language.Name,
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverImageURL = book.CoverImageUrl,
                     Gallery = book.bookGallery.Select(x => new GalleryModel()
                     {
                         Name = x.Name,
                         Id = x.Id,
                         URL = x.URL
                     }).ToList(),
                     BookPDFUrl = book.BookPdfUrl


                 }).FirstOrDefaultAsync();

            return bookData;
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }


        public List<BookModel> SearchBook(string title, string author)
        {
            return null;
        }

        public string GetAppName()
        {
            return configuration["AppName"];
        }

    }
}
