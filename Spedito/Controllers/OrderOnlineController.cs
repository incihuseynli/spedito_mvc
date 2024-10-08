using Microsoft.AspNetCore.Mvc;

namespace Spedito.Controllers
{
    public class OrderOnlineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
