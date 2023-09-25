using Microsoft.AspNetCore.Mvc;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] User user)
        {
            if(user == null) 
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("message", "user could not be saved");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully user created");
        }
    }
}
