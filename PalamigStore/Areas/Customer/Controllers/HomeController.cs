using Microsoft.AspNetCore.Mvc;

namespace PalamigStore.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
