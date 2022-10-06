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
                TotalPages = model.TotalPages,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };

            await _context.books.AddAsync(books);
            await _context.SaveChangesAsync();

            return books.Id;

        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.books.ToListAsync();

            if (allBooks?.Any() == true)
            {

                foreach (var book in allBooks)
                {
                    var bookData = new BookModel();

                    bookData.Author = book.Author;
                    bookData.Title = book.Title;
                    bookData.Description = book.Description;
                    bookData.TotalPages = book.TotalPages;
                    bookData.Category = book.Category;
                    bookData.Id = book.Id;
                    bookData.Language = book.Language;
                    bookData.CreatedOn = book.CreatedOn;
                    bookData.UpdatedOn = book.UpdatedOn;

                    books.Add(bookData);
                }

            }
            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {

            var book= await _context.books.FindAsync(id);

            if (book != null)
            {
                var bookData = new BookModel();

                bookData.Author = book.Author;
                bookData.Title = book.Title;
                bookData.Description = book.Description;
                bookData.TotalPages = book.TotalPages;
                bookData.Category = book.Category;
                bookData.Id = book.Id;
                bookData.Language = book.Language;
                bookData.CreatedOn = book.CreatedOn;
                bookData.UpdatedOn = book.UpdatedOn;

                return bookData;
            }

            return null;
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }


        public List<BookModel> SearchBook(string title, string author)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(author)).ToList();
        }


        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                 new BookModel(){Id =1, Title="MVC", Author = "Nitish", Description="This is the description for MVC book", Category="Programming", Language="English", TotalPages=134 },
                new BookModel(){Id =2, Title="Dot Net Core", Author = "Nitish", Description="This is the description for Dot Net Core book", Category="Framework", Language="Chinese", TotalPages=567 },
                new BookModel(){Id =3, Title="C#", Author = "Monika", Description="This is the description for C# book", Category="Developer", Language="Hindi", TotalPages=897 },
                new BookModel(){Id =4, Title="Java", Author = "Webgentle", Description="This is the description for Java book", Category="Concept", Language="English", TotalPages=564 },
                new BookModel(){Id =5, Title="Php", Author = "Webgentle", Description="This is the description for Php book", Category="Programming", Language="English", TotalPages=100 },
                new BookModel(){Id =6, Title="Azure DevOps", Author = "Nitish", Description="This is the description for Azure Devops book", Category="DevOps", Language="English", TotalPages=800 }

            };
        }
    }
}
