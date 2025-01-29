using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    public class OrderbookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
