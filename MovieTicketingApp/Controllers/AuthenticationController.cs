using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingApp.Authentication.Models;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using MovieTicketingApp.Services.PasswordHasher;
using MovieTicketingApp.Services.TokenGenerators;
using MovieTicketingApp.Services.TokenValidators;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IMapper _mapper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationController(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            RefreshTokenValidator refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            if (userRegister == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userRegister.Password != userRegister.ConfirmPassword)
            {
                ModelState.AddModelError("message", "Passwords doesn't match");
                return BadRequest(ModelState);
            }

            User user = _mapper.Map<User>(userRegister);

            if (_userRepository.UserExists(user))
            {
                ModelState.AddModelError("message", "User already exists");
                return Conflict(ModelState);
            }

            string passwordHash = _passwordHasher.HashPassword(user.Password);
            user.Password = passwordHash;

            if (!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("message", "user can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok(new Response()
            {
                Success="Successfully user created"
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = _mapper.Map<User>(userLogin);

            if (!_userRepository.UserExists(user.Username))
                return Unauthorized();

            user = _userRepository.GetUser(userLogin.Username);

            bool passwordMatch = _passwordHasher.VerifyPassword(userLogin.Password, user.Password);

            if (!passwordMatch)
                return Unauthorized();

            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDto = new();
            refreshTokenDto.User = user;
            refreshTokenDto.Token = refreshToken;

            if (!_refreshTokenRepository.AddRefreshToken(refreshTokenDto))
            {
                ModelState.AddModelError("message", "Token can not be created");
                return BadRequest(ModelState);
            }

            UserDto userDto = _mapper.Map<UserDto>(user);

            return Ok(new AuthenticatedUser()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = userDto
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Refresh([FromBody] RefreshTokenString refreshTokenString) 
        {
            if (refreshTokenString == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string refreshToken = refreshTokenString.refreshToken;

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshToken);

            if (!isValidRefreshToken)
            {
                ModelState.AddModelError("message", "Token is invalid");
                return BadRequest(ModelState);
            }

            if (!_refreshTokenRepository.RefreshTokenExists(refreshToken))
            {
                ModelState.AddModelError("message", "Token doesn't exists");
                return BadRequest(ModelState);
            }

            RefreshToken refreshTokenDto = _refreshTokenRepository.GetRefreshToken(refreshToken);
            User user = _userRepository.GetUser(refreshTokenDto.UserId);

            if (user == null)
            {
                {
                    ModelState.AddModelError("message", "User doesn't exists");
                    return BadRequest(ModelState);
                }
            }

            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string newRefreshTokenString = _refreshTokenGenerator.GenerateToken();
            refreshTokenDto.Token = newRefreshTokenString;

            if (!_refreshTokenRepository.UpdateRefreshToken(refreshTokenDto))
            {
                ModelState.AddModelError("message", "Token can not be updated");
                return BadRequest(ModelState);
            }

            UserDto userDto = _mapper.Map<UserDto>(user);

            return Ok(new AuthenticatedUser()
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshTokenString,
                User = userDto
            });
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Logout([FromBody] RefreshTokenString refreshTokenString)
        {
            if (refreshTokenString == null)
            {
                return BadRequest();
            }

            string refreshToken = refreshTokenString.refreshToken;

            if (!_refreshTokenRepository.RefreshTokenExists(refreshToken))
            {
                ModelState.AddModelError("message", "Token doesn't exist");
                return BadRequest(ModelState);
            }

            RefreshToken refreshTokenDto = _refreshTokenRepository.GetRefreshToken(refreshToken);
            IEnumerable<RefreshToken> refreshTokens = _refreshTokenRepository.GetAllRefreshTokens(refreshTokenDto.UserId);
            
            if (!refreshTokens.Any())
            {
                ModelState.AddModelError("message", "Token doesn't exists");
                return BadRequest(ModelState);
            }

            if (!_refreshTokenRepository.DeleteAllRefreshToken(refreshTokens))
            {
                ModelState.AddModelError("message", "Tokens can not be deleted");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
