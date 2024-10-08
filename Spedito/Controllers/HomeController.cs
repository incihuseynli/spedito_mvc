using Microsoft.AspNetCore.Mvc;
using Spedito.Models;
using System.Diagnostics;

namespace Spedito.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
