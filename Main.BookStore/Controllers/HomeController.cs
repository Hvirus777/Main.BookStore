using Main.BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Dynamic;

namespace Main.BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }




        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public string PageTitle { get; set; } 
        
        // Passing Complex Data from controller to View using ViewDataAttribute
        [ViewData]
        public BookModel bookModel{ get; set; }



        // ~ sign will override the setting of routing done at controller level above controller. If remove ~ then it will show 404
        [Route("~/")]
        //[Route("home/index")]   // pattern for attribute routing using mapcontroller endpoint but problem here is if there is any change in name of action or controller in future then this pattern needs to be updated as well. SO to resolve this we use token routing as shown below.


      //  [Route("[controller]/[action]")]  // Duplicacy issue because need to write it over all action method so just remove it and write it above controller. ( Controller Level attribute Routing )
        public ViewResult Index()
        {
            var res = configuration["AppName"];
            var key1 = configuration["infoOB:Key1"];
            var key2 = configuration["infoOB:Key2"];
            var key3 = configuration["infoOB:Key3:Key3of3"];


            dynamic data = new ExpandoObject(); // ExpandoObject is used to pass Anonymous Object
            data.id = 1;
            data.name = "Harsh";

            ViewBag.Data = data;

            bookModel = new BookModel() { Id = 1, Title = "Asp.net Core Tutorial" };


            CustomProperty = "Custome Value";
            PageTitle = "Home";

            return View();
        }

        [HttpGet("~/about-us")]  //Attribute Routing using HttpVerb + routing
        public ViewResult AboutUs()
        {
            PageTitle = "About Us";
            return View();
        }
        [Route("~/contact-us")] // Normal Attribute Routing
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
