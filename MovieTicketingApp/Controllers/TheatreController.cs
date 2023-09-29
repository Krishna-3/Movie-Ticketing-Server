﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreRepository _theatreRepository;
        private readonly IMapper _mapper;
        private readonly IStateRepository _state;
        private readonly ILocationRepository _locationRepository;

        public TheatreController(ITheatreRepository theatreRepository,
                                 IMapper mapper,
                                 IStateRepository state,
                                 ILocationRepository locationRepository)
        {
            _theatreRepository = theatreRepository;
            _mapper = mapper;
            _state = state;
            _locationRepository = locationRepository;
        }

        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TheatreEnDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TheatreTeDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TheatreHiDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTheatres(int movieId)
        {
            if (_state.GetLocation().IsNullOrEmpty())
            {
                ModelState.AddModelError("message", "Please select a location");
                return BadRequest(ModelState);
            }

            var city = _state.GetLocation();
            var theatres = _theatreRepository.GetTheatresForMovie(movieId, city);

            if (theatres.IsNullOrEmpty())
            {
                ModelState.AddModelError("message", "No Theatres");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var language = _state.GetLanguage();

            if (language == "te")
            {
                var teTheatres = _mapper.Map<List<TheatreTeDto>>(theatres);

                return Ok(teTheatres);
            }
            else if (language == "hi")
            {
                var hiTheatres= _mapper.Map<List<TheatreHiDto>>(theatres);

                return Ok(hiTheatres);

            }
            else
            {
                var enTheatres = _mapper.Map<List<TheatreEnDto>>(theatres);

                return Ok(enTheatres);
            }
        }

        [HttpGet("theatre/{theatreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TheatreEnDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TheatreTeDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TheatreHiDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTheatre(int theatreId)
        {
            
            var theatre = _theatreRepository.GetTheatre(theatreId);

            if (theatre == null)
            {
                ModelState.AddModelError("message", "No Theatres");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var language = _state.GetLanguage();

            if (language == "te")
            {
                var teTheatre = _mapper.Map<TheatreTeDto>(theatre);

                return Ok(teTheatre);
            }
            else if (language == "hi")
            {
                var hiTheatre = _mapper.Map<TheatreHiDto>(theatre);

                return Ok(hiTheatre);

            }
            else
            {
                var enTheatre = _mapper.Map<TheatreEnDto>(theatre);

                return Ok(enTheatre);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Theatre>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllMovies()
        {
            var theatres = _theatreRepository.GetAllTheatres();

            return Ok(theatres);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTheatre([FromBody] TheatreName theatreName, [FromQuery] int locationId)
        {
            if (theatreName == null || locationId < 0 ) 
                return BadRequest();

            if (_theatreRepository.TheatreExists(theatreName))
            {
                ModelState.AddModelError("message", "Theatre already exists");
                return BadRequest(ModelState);
            }

            if (!_locationRepository.LocationExists(locationId))
            {
                ModelState.AddModelError("message", "Location doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theatreMap = _mapper.Map<Theatre>(theatreName);
            theatreMap.LocationId = locationId;

            if (!_theatreRepository.CreateTheatre(theatreMap))
            {
                ModelState.AddModelError("message", "Theatre can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully theatre created");
        }

        [HttpPatch("name/{theatreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTheatreName(int theatreId, [FromBody] TheatreName Name)
        {
            if (theatreId < 0)
                return BadRequest();

            if (Name == null)
                return BadRequest();

            if (!_theatreRepository.TheatreExists(theatreId))
            {
                ModelState.AddModelError("message", "Theatre doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theatre = _theatreRepository.GetTheatre(theatreId);
            theatre.NameTe = Name.NameTe;
            theatre.NameHi = Name.NameHi;
            theatre.NameEn = Name.NameEn;

            if (!_theatreRepository.UpdateTheatre(theatre))
            {
                ModelState.AddModelError("message", "Theatre can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("location/{theatreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTheatreLocation(int theatreId, [FromBody] int locationId)
        {
            if (theatreId < 0 || locationId < 0)
                return BadRequest();

            if (!_locationRepository.LocationExists(locationId))
                return BadRequest();

            if (!_theatreRepository.TheatreExists(theatreId))
            {
                ModelState.AddModelError("message", "Theatre doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theatre = _theatreRepository.GetTheatre(theatreId);
            theatre.LocationId = locationId;

            if (!_theatreRepository.UpdateTheatre(theatre))
            {
                ModelState.AddModelError("message", "Theatre can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{theatreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTheatre(int theatreId)
        {
            if (theatreId < 0)
                return BadRequest();

            if (!_theatreRepository.TheatreExists(theatreId))
            {
                ModelState.AddModelError("message", "Theatre doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theatre = _theatreRepository.GetTheatre(theatreId);

            if (!_theatreRepository.DeleteTheatre(theatre))
            {
                ModelState.AddModelError("message", "Theatre can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}