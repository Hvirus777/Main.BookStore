using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Main.BookStore.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {
            dynamic data = new ExpandoObject();
            data.id = 1;
            data.name = "Harsh";

            ViewBag.Data = data;

            return View();
        }
        public ViewResult AboutUs()
        {
            return View();
        }
        public ViewResult ContactUs()
        {
            return View();
        }

        public string Indexs()
        {
            return "Hey its from Home Controller!";
        }
    }
}
