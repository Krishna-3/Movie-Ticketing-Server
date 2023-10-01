using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovieTicketingApp.Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly AuthenticationConfiguraiton _configuration;

        public RefreshTokenGenerator(AuthenticationConfiguraiton configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken()
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.RefreshTokenSecret));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                _configuration.Issuer,
                _configuration.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_configuration.RefreshTokenExpirationDays),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
