using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DesignPatterns_Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => Ok());
        }
    }
}
