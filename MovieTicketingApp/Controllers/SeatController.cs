using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;

        public SeatController(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Seat>))]
        [ProducesResponseType(400)]
        public IActionResult GetSeats()
        {
            var seats = _seatRepository.GetSeats();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seats);
        }
    }
}
