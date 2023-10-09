using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using System.Security.Claims;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Statecontroller : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILocationRepository _location;

        public Statecontroller(IStateRepository stateRepository, ILocationRepository location)
        {
            _stateRepository = stateRepository;
            _location = location;
        }

        [AllowAnonymous]
        [HttpPost("language")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLanguage ([FromQuery] string languageCode)
        {
            int userId = Int32.Parse(HttpContext.User.FindFirstValue("Id"));

            if (languageCode.IsNullOrEmpty())
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allowedCodes = _stateRepository.GetAllowedLanguageCodes();

            if (!allowedCodes.Contains(languageCode))
            {
                ModelState.AddModelError("message", "language is not supported");
                return BadRequest(ModelState);
            }

            if (!_stateRepository.StateExists(userId))
            {
                if (!_stateRepository.CreateState(userId))
                {
                    ModelState.AddModelError("message", "Can't select language");
                    return BadRequest(ModelState);
                }
            }

            if (!_stateRepository.SetLanguage(languageCode, userId))
                return BadRequest(ModelState);

            return Ok("Successfully langauge selected");
        }

        [AllowAnonymous]
        [HttpPost("location")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLocation ([FromQuery] string city)
        {
            int userId = Int32.Parse(HttpContext.User.FindFirstValue("Id"));

            if (city.IsNullOrEmpty())
                return BadRequest();

            if (!_location.LocationExists(city))
            {
                ModelState.AddModelError("message", "Location does not exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_stateRepository.StateExists(userId))
            {
                if (!_stateRepository.CreateState(userId))
                {
                    ModelState.AddModelError("message", "Can't select location");
                    return BadRequest(ModelState);
                }
            }

            _stateRepository.SetLocation(city, userId);

            return Ok("Successfully location selected");
        }
    }
}
