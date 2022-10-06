using Main.BookStore.Data;
using Main.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Main.BookStore.Repository
{
    public class BookRepository
    {

        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var books = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                LanguageId = model.LanguageId
            };

            await _context.books.AddAsync(books);
            await _context.SaveChangesAsync();

            return books.Id;

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
                TotalPages = book.TotalPages

            }).ToListAsync();
             
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
                     TotalPages = book.TotalPages
                 }).FirstOrDefaultAsync();

            return bookData;
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }


        public List<BookModel> SearchBook(string title, string author)
        {
            return null;
        }



    }
}
