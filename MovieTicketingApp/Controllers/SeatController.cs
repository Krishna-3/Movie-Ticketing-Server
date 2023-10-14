using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using System.Globalization;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public SeatController(ISeatRepository seatRepository, ITicketRepository ticketRepository, IStateRepository stateRepository, IMapper mapper)
        {
            _seatRepository = seatRepository;
            _ticketRepository = ticketRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("{timeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Seat>))]
        [ProducesResponseType(400)]
        public IActionResult GetSeats(int timeId, [FromBody] TicketId ticketId, [FromQuery] string date)
        {
            var seats = _seatRepository.GetSeats();
            
            var seatsDto=_mapper.Map<List<SeatDto>>(seats);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ticketId == null || timeId < 1 || timeId > 4 || date == null)
                return BadRequest();

            var timings = _stateRepository.GetTimings();
            var time = timings[timeId.ToString()];

            if (time == null)
                return BadRequest();

            string dateTime = date + ", " + time;

            if (dateTime == null)
                return BadRequest(dateTime);

            string format = "dd-MM-yyyy, h:mmtt";

            DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

            var now = DateTime.Now;

            if (now > result)
            {
                ModelState.AddModelError("message", "can not buy ticket for that time");
                return BadRequest(ModelState);
            }

            var tickets = _ticketRepository.GetBookedTickets(ticketId,result);            

            if (!tickets.IsNullOrEmpty())
            {
                foreach(var t in tickets)
                {
                    var seat = seatsDto.First(s => s.Id == t.SeatId);
                    seat.Booked = true;
                }
            }

            return Ok(seatsDto);
        }
    }
}