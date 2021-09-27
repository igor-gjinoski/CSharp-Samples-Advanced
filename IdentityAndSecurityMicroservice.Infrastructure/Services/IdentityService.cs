using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IdentityAndSecurityMicroservice.MongoDB.Entities;
using IdentityAndSecurityMicroservice.Application;
using IdentityAndSecurityMicroservice.Application.RequestModels;
using IdentityAndSecurityMicroservice.Application.ResponseModels;

namespace IdentityAndSecurityMicroservice.Services 
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _appSettings;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }


        public async Task<RegisterSuccessModel> RegisterAsync(RegisterRequestModel requestModel, CancellationToken cancellationToken)
        {
            ApplicationUser user = new();
            await _userManager.CreateAsync(user, requestModel.Password);
            return new RegisterSuccessModel(user.Id);
        }


        public async Task<LoginSuccessModel> LoginAsync(LoginRequestModel requestModel, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(requestModel.Username);
            await _userManager.CheckPasswordAsync(user, requestModel.Password);

            var token = GenerateJwtToken(user);
            return new LoginSuccessModel(token);
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = System.DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
