using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns_Repository.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
