
namespace IdentityAndSecurityMicroservice.Application.ResponseModels
{
    public class LoginSuccessModel
    {
        public LoginSuccessModel(string token)
            => Token = token;

        public string Token { get; set; }
    }
}
