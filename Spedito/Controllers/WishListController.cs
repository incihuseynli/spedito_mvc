using Microsoft.AspNetCore.Mvc;

namespace Spedito.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
