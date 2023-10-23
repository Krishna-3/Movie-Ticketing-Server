using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using MovieTicketingApp.Services.PasswordHasher;
using System.Security.Claims;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserController(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [Authorize]
        [HttpPatch("username/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUsername(int userId, [FromQuery] string username)
        {
            if (userId < 0)
                return BadRequest();

            if (username.IsNullOrEmpty())
                return BadRequest();

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("message", "User doesn't exists");
                return BadRequest(ModelState);
            }

            if (_userRepository.UserExists(username))
            {
                ModelState.AddModelError("message", "Username is taken");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUser(userId);
            user.Username = username;
             
            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("message", "user can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [HttpPatch("password/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePassword(int userId, 
            [FromBody] Password passwords)
        {
            if (userId < 0)
                return BadRequest();

            if (passwords == null)
                return BadRequest();

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("message", "User doesn't exists");
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(userId);

            if (!_passwordHasher.VerifyPassword(passwords.prevPassword, user.Password))
            {
                ModelState.AddModelError("message", "Unauthorized");
                return StatusCode(401,ModelState);
            }

            user.Password = _passwordHasher.HashPassword(passwords.newPassword);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("message", "user can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            int id = Int32.Parse(HttpContext.User.FindFirstValue("Id"));

            if (id != userId)
            {
                return Unauthorized();
            }

            if (userId < 0)
                return BadRequest();

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("message", "User doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUser(userId);

            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("message", "user can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
