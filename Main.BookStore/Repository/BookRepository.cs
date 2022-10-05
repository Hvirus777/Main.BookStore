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
                new BookModel(){Id=1,Title="MVC",Author="Nitish",Description="This is the description of MVC Book" },
                new BookModel(){Id=2,Title="Dot Net Core",Author="Nitish",Description="This is the description of Dot Net Core Book" },
                new BookModel(){Id=3,Title="C#",Author="Monica",Description="This is the description of C# Book" },
                new BookModel(){Id=4,Title="JAVA",Author="Harsh" ,Description="This is the description of JAVA Book"},
                new BookModel(){Id=5,Title="JAVA",Author="Harsh" ,Description="This is the description of JAVA Book"},
                new BookModel(){Id=6,Title="Azure DevOps",Author="Nitish",Description="This is the description of Azure DevOps Book" }
            };
        }
    }
}
