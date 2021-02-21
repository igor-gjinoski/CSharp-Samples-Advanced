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
            var fileSystemLog = new FileSystemLogSpecification("LOG");
            var repository = new LoggerRepository();

            // TODO:
            bool log = repository.LogInfo(fileSystemLog);

            return Ok();
        }
    }
}
