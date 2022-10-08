using Main.BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.BookStore.Controllers
{
    public class AccountController : Controller
    {


        [Route("sign-up")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost("sign-up")]        
        public IActionResult Signup(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {


                ModelState.Clear();
            }
            else
            {

            }


            return View();
        }
    }
}
