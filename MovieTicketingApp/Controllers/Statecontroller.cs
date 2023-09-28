using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Interfaces;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Statecontroller : ControllerBase
    {
        private readonly IStateRepository _state;
        private readonly ILocationRepository _location;

        public Statecontroller(IStateRepository state, ILocationRepository location)
        {
            _state = state;
            _location = location;
        }

        [HttpPost("language")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLanguage ([FromBody] string languageCode)
        {
            _state.SetLanguage("en");

            if (languageCode.IsNullOrEmpty())
                return BadRequest();

            var allowedCodes = _state.GetAllowedLanguageCodes();

            if (!allowedCodes.Contains(languageCode))
            {
                ModelState.AddModelError("message", "language is not supported");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _state.SetLanguage(languageCode);

            return Ok("Successfully langauge selected");
        }

        [HttpPost("location")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLocation ([FromBody] string city)
        {
            if (city.IsNullOrEmpty())
                return BadRequest();

            if (!_location.LocationExists(city))
            {
                ModelState.AddModelError("message", "Location does not exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _state.SetLocation(city);

            return Ok("Successfully location selected");
        }
    }
}
