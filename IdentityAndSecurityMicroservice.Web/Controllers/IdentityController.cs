using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityAndSecurityMicroservice.Application.RequestModels;
using IdentityAndSecurityMicroservice.Application.ResponseModels;
using IdentityAndSecurityMicroservice.Services;

namespace IdentityAndSecurityMicroservice.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
            =>
            _identityService = identityService;


        [HttpPost]
        public async Task<RegisterSuccessModel> Register(
            RegisterRequestModel requestModel,
            CancellationToken cancellationToken)
        {
            return await _identityService.RegisterAsync(requestModel, cancellationToken);
        }

        [HttpGet]
        public async Task<LoginSuccessModel> Login(
            LoginRequestModel requestModel,
            CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(requestModel, cancellationToken);
        }
    }
}
