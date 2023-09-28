using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private Language _language;

        public LanguageController(Language language)
        {
            _language = language;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SelectLanguage ([FromBody] string languageCode)
        {
            _language.preferredLanguage = "en";

            if (languageCode.IsNullOrEmpty())
                return BadRequest();

            if (!_language.LanguageCode.Contains(languageCode))
            {
                ModelState.AddModelError("message", "language is not supported");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _language.preferredLanguage = languageCode;

            return Ok("Successfully user created");
        }
    }
}
