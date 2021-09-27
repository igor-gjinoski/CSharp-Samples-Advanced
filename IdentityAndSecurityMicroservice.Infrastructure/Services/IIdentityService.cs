using IdentityAndSecurityMicroservice.Application.RequestModels;
using IdentityAndSecurityMicroservice.Application.ResponseModels;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityAndSecurityMicroservice.Services
{
    public interface IIdentityService
    {
        Task<RegisterSuccessModel> RegisterAsync(RegisterRequestModel requestModel, CancellationToken cancellationToken);

        Task<LoginSuccessModel> LoginAsync(LoginRequestModel requestModel, CancellationToken cancellationToken);
    }
}
