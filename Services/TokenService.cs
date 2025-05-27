using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using absensi_api.ViewModels;
using Microsoft.IdentityModel.Tokens;


namespace absensi_api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) => _config = config;

        public string CreateToken(AuthenticationViewModel.Payload payload)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, payload.name)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}