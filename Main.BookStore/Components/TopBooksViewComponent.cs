using Main.BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Main.BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly BookRepository _bookRepository = null;
        public TopBooksViewComponent(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
         
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBookAsync(count);

            return View(books);
        }

    }
}
