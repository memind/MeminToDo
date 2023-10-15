using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomePages.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> TestPage()
        {
            Console.WriteLine("------------------------------------");

            var prop = (await HttpContext.AuthenticateAsync()).Properties.Items;
            foreach (var claim in User.Claims)
            {
                Console.WriteLine(claim);
            }

            Console.WriteLine("------------------------------------");

            foreach (var item in prop)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("------------------------------------");
            return View();
        }
    }
}