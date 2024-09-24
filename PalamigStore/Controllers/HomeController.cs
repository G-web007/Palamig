using Microsoft.AspNetCore.Mvc;

namespace PalamigStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
