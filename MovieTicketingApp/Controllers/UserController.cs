using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpPatch("username/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUsername(int userId, [FromBody] string username)
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

            if (passwords.newPassword.IsNullOrEmpty())
            {
                ModelState.AddModelError("message", "password is required");
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("message", "User doesn't exists");
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(userId);

            if (user.Password != passwords.prevPassword)
            {
                ModelState.AddModelError("message", "Unauthorized");
                return StatusCode(401,ModelState);
            }

            user.Password = passwords.newPassword;

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
