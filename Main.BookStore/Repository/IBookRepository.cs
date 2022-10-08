using Main.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBookAsync(int count);
        List<BookModel> SearchBook(string title, string author);

        string GetAppName();
    }
}