using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using IdentityAndSecurityMicroservice.RequestModel;
using IdentityAndSecurityMicroservice.Entities;

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


        public async Task<IActionResult> RegisterAsync(RegisterRequestModel requestModel, CancellationToken cancellationToken)
        {
            ApplicationUser user = new ApplicationUser();

            var identityResult = await _userManager.CreateAsync(user, requestModel.Password);
            var result = new RegisterSuccessModel(identityResult.Succeeded, identityResult.Errors);

            return result;
        }


        public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(requestModel.Username);
            bool passwordValid = await _userManager.CheckPasswordAsync(user, requestModel.Password);

            var token = GenerateJwtToken(user);
            var result = new LoginSuccessModel(user.Id, token);

            return result;
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
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
