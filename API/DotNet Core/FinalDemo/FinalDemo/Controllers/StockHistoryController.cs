using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    public class StockHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
