using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovieTicketingApp.Services.TokenValidators
{
    public class RefreshTokenValidator
    {
        private readonly AuthenticationConfiguraiton _configuration;

        public RefreshTokenValidator(AuthenticationConfiguraiton configuraiton)
        {
            _configuration = configuraiton;            
        }

        public bool Validate(string refreshToken)
        {
            TokenValidationParameters validationParameters =new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.RefreshTokenSecret)),
                ValidIssuer = _configuration.Issuer,
                ValidAudience = _configuration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new();
            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
