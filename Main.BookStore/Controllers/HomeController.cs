using Microsoft.AspNetCore.Mvc;

namespace Main.BookStore.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {
            return View();
        } 
        public ViewResult AboutUs()
        {
            return View();
        }

        public string Indexs()
        {
            return "Hey its from Home Controller!";
        }
    }
}
