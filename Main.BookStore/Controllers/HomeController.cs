using Main.BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Main.BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public string PageTitle { get; set; } 
        
        // Passing Complex Data from controller to View using ViewDataAttribute
        [ViewData]
        public BookModel bookModel{ get; set; }

        public ViewResult Index()
        {
            dynamic data = new ExpandoObject(); // ExpandoObject is used to pass Anonymous Object
            data.id = 1;
            data.name = "Harsh";

            ViewBag.Data = data;

            bookModel = new BookModel() { Id = 1, Title = "Asp.net Core Tutorial" };


            CustomProperty = "Custome Value";
            PageTitle = "Home";

            return View();
        }
        public ViewResult AboutUs()
        {
            PageTitle = "About Us";
            return View();
        }
        public ViewResult ContactUs()
        {
            PageTitle = "Contact Us";
            return View();
        }

        public string Indexs()
        {
            return "Hey its from Home Controller!";
        }
    }
}
