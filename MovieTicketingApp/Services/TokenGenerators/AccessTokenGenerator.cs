using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Authentication.Models;
using MovieTicketingApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieTicketingApp.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguraiton _configuration;

        public AccessTokenGenerator(AuthenticationConfiguraiton configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.AccessTokenSecret));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Name", user.Username),
                new Claim("Role", user.Role)
            };

            JwtSecurityToken token = new(
                _configuration.Issuer,
                _configuration.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);  
        }
    }
}
