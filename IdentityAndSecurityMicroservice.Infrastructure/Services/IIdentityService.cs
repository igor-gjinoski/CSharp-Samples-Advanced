using IdentityAndSecurityMicroservice.RequestModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityAndSecurityMicroservice.Services
{
    public interface IIdentityService
    {
        Task<IActionResult> RegisterAsync(RegisterRequestModel requestModel, CancellationToken cancellationToken);

        Task<IActionResult> LoginAsync(LoginRequestModel requestModel, CancellationToken cancellationToken);
    }
}
