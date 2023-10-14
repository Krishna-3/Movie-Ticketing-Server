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
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IStateRepository _stateRepository;
        private readonly IMovieTheatreRepository _movieTheatreRepository;

        public TicketController(ITicketRepository ticketRepository, IMapper mapper, IStateRepository stateRepository, IMovieTheatreRepository movieTheatreRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _stateRepository = stateRepository;
            _movieTheatreRepository = movieTheatreRepository;
        }

        [Authorize]
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTickets(int userId)
        {
            var tickets = _ticketRepository.GetTickets(userId);

            if (tickets.IsNullOrEmpty())
            {
                return NotFound();
            }

            for(int i = 0; i < tickets.Count(); i++)
            {
                var photo = tickets.ElementAt(0).MovieTheatre.Movie.Photo;
                if (photo!= null)
                {
                    tickets.ElementAt(0).MovieTheatre.Movie.Photo = GetImage(Convert.ToBase64String(photo));
                }
            }

            var ticketsMap = _mapper.Map<List<TicketDto>>(tickets);

            return Ok(ticketsMap);
        }

        private static byte[] GetImage(string PhotoString)
        {
            byte[] bytes = null;

            if (!string.IsNullOrEmpty(PhotoString))
            {
                bytes = Convert.FromBase64String(PhotoString);
            }

            return bytes;
        }

        [Authorize]
        [HttpPost("{timeId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTicket(int timeId, [FromBody] TicketId ticketId, [FromQuery] string date)
        {
            if (ticketId == null || timeId < 1 || timeId > 4 || date == null)
                return BadRequest();

            var timings = _stateRepository.GetTimings();
            var time = timings[timeId.ToString()];

            if (time == null)
                return BadRequest();

            string dateTime = date+", "+time;

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

            if (_ticketRepository.TicketExists(ticketId, result))
            {
                ModelState.AddModelError("message", "Ticket already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = _mapper.Map<Ticket>(ticketId);
            ticket.Time = result;
            ticket.MovieTheatreId = _movieTheatreRepository.GetMovieTheatre(ticketId.MovieId,ticketId.TheatreId).Id;

            if (!_ticketRepository.CreateTicket(ticket))
            {
                ModelState.AddModelError("message", "Ticket can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok(new Response()
            {
                Success= "Successfully ticket created"
            });
        }
    }
}
