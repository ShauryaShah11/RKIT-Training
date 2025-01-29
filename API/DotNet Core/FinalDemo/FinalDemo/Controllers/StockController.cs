using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
