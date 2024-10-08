using Microsoft.AspNetCore.Mvc;

namespace Spedito.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
