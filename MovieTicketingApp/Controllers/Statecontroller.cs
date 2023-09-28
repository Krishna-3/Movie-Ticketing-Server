using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Models;
using MovieTicketingApp.Repository;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Statecontroller : ControllerBase
    {
        private readonly State _state;
        private readonly LocationRepository _location;

        public Statecontroller(State state, LocationRepository location)
        {
            _state = state;
            _location = location;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLanguage ([FromBody] string languageCode)
        {
            _state.preferredLanguage = "en";

            if (languageCode.IsNullOrEmpty())
                return BadRequest();

            if (!_state.LanguageCode.Contains(languageCode))
            {
                ModelState.AddModelError("message", "language is not supported");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _state.preferredLanguage = languageCode;

            return Ok("Successfully langauge selected");
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLocation ([FromBody] string city)
        {
            if (city.IsNullOrEmpty())
                return BadRequest();

            if (_location.LocationExists(city))
            {
                ModelState.AddModelError("message", "Location does not exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _state.selectedLocation = city;

            return Ok("Successfully location selected");
        }
    }
}
