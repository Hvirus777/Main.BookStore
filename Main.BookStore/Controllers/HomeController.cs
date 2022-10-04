using Microsoft.AspNetCore.Mvc;

namespace Main.BookStore.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hey its from Home Controller!";
        }
    }
}
