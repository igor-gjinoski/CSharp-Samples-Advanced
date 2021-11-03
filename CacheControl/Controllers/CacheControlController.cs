using CacheControl.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CacheControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheControlController : Controller
    {
        [HttpGet]
        [CacheControl(Attributes.ResponseCacheLocation.Client, 60)]
        public IActionResult Index() => Ok();

        // Or use:
        // [ResponseCache(CacheProfileName = "public", Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Client, Duration = 60)]
    }
}
