using Microsoft.AspNetCore.Mvc;
using DesignPatterns_Specification.Implementation;

namespace DesignPatterns_Specification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var gRating = new FileSystemLogSpecification();
            var repository = new LoggerRepository();

            // TODO:

            return Ok();
        }
    }
}
