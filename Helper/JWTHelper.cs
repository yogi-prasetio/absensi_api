using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using absensi_api.ViewModels;
using System.Text.Json;
using System.Security.Claims;
using System.Text;


namespace absensi_api.Helper
{
    public class JWTHelper
    {
        public static string GenerateToken(AuthenticationViewModel.Payload payload, IConfiguration config, string? context = null)
        {
            var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            // var key = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            var issuedAt = DateTime.UtcNow;
            // JwtSecurityToken token;
            // if (context == "Login")
            // {
            //     var expires = issuedAt.AddHours(5);
            //     token = new JwtSecurityToken(
            //         issuer: config["Jwt:Issuer"],
            //         audience: config["Jwt:Audience"],
            //     claims: new[]
            //         {
            //         new System.Security.Claims.Claim("sub", config["Jwt:Subject"]),
            //         new System.Security.Claims.Claim("account_id", payload.account_id.ToString()),
            //         new System.Security.Claims.Claim("name", payload.name),
            //         },
            //         expires: expires,
            //         notBefore: issuedAt,
            //         signingCredentials: key
            //     );
            // }
            // else
            // {
            //     var expires = issuedAt.AddHours(1);
            //     token = new JwtSecurityToken(
            //         issuer: config["Jwt:Issuer"],
            //         audience: config["Jwt:Audience"],
            //         expires: expires,
            //         notBefore: issuedAt,
            //         signingCredentials: key
            //     );
            // }
            Claim roleClaim = new Claim(ClaimTypes.Role, payload.role_name);
            var claims = new[]
        {
                        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", payload.email),
                        new Claim("Password", ""),
                        new Claim("AccountId", payload.account_id.ToString()),
                        new Claim("RoleId", payload.role_id.ToString()),
                        new Claim("Name", payload.name),

                        roleClaim
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: signIn);
            var userToken = new JwtSecurityTokenHandler().WriteToken(token);

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenResult;
        }
        public static Dictionary<string, object> Decode(string token, IConfiguration config, bool verifyToken = false)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
            var key = config["Jwt:Key"];

            var payloadJson = decoder.Decode(token, key, verify: verifyToken);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);
        }
    }
}