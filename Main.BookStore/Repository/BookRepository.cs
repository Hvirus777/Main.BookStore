using Main.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Main.BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }


        public List<BookModel> SearchBook(string title, string author)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(author)).ToList();
        }


        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1,Title="MVC",Author="Nitish" },
                new BookModel(){Id=2,Title="Dot Net Core",Author="Nitish" },
                new BookModel(){Id=3,Title="C#",Author="Monica" },
                new BookModel(){Id=4,Title="JAVA",Author="Harsh" },
                new BookModel(){Id=5,Title="PHP",Author="Harsh" }
            };
        }
    }
}
