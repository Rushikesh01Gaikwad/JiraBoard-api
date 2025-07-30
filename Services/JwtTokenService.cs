using Azure.Core;
using JiraBoard_api.Modals;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JiraBoard_api.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public TokenModel generateToken(User user)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    //new Claim(ClaimTypes.Role, "Manager")
                };

                var expiration = DateTime.Now.AddMinutes(120); // Short-lived access token
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: cred);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                var randomBytes = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomBytes);
                var refreshToken =  Convert.ToBase64String(randomBytes);

                return new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Expiration = expiration
                };

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
