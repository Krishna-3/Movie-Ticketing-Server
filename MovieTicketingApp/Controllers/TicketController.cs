﻿using AutoMapper;
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

        public TicketController(ITicketRepository ticketRepository, IMapper mapper, IStateRepository stateRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _stateRepository = stateRepository;
        }

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

            var ticketsMap = _mapper.Map<List<TicketDto>>(tickets);

            return Ok(ticketsMap);
        }

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

            string dateTime=date + ", " + time;
            string format = "d-M-yyyy, h:mm tt";

            DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

            if (_ticketRepository.TicketExists(ticketId, result))
            {
                ModelState.AddModelError("message", "Ticket already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = _mapper.Map<Ticket>(ticketId);
            ticket.Time = result;

            if (!_ticketRepository.CreateTicket(ticket))
            {
                ModelState.AddModelError("message", "Ticket can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully ticket created");
        }
    }
}
