using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using MovieTicketingApp.Repository;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieTheatreController : ControllerBase
    {
        private readonly IMovieTheatreRepository _movieTheatreRepository;
        private readonly IMapper _mapper;
        private readonly IMovieLocationRepository _movieLocationRepository;
        private readonly ITheatreRepository _theatreRepository;

        public MovieTheatreController(IMapper mapper, IMovieTheatreRepository movieTheatreRepository, IMovieLocationRepository movieLocationRepository, ITheatreRepository theatreRepository)
        {
            _movieTheatreRepository = movieTheatreRepository;
            _mapper = mapper;
            _movieLocationRepository = movieLocationRepository;
            _theatreRepository = theatreRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllMovieTheatres()
        {
            var movieLocations = _movieTheatreRepository.GetAllMovieTheatres();

            return Ok(movieLocations);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovieTheatre(MovieTheatreDto movieTheatreDto)
        {
            if (movieTheatreDto == null)
                return BadRequest();

            if (_movieTheatreRepository.MovieTheatreExists(movieTheatreDto))
            {
                ModelState.AddModelError("message", "Movie-Theatre already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var theatre = _theatreRepository.GetTheatre(movieTheatreDto.TheatreId);
            if (theatre == null)
            {
                ModelState.AddModelError("message", "Theatre not found");
                return BadRequest(ModelState);
            }
            var locationId = theatre.LocationId;

            if (!_movieLocationRepository.MovieLocationExists(movieTheatreDto.MovieId, locationId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists in the specified location");
                return BadRequest(ModelState);
            }

            var movieTheatre= _mapper.Map<MovieTheatre>(movieTheatreDto);

            if (!_movieTheatreRepository.CreateMovieTheatre(movieTheatre))
            {
                ModelState.AddModelError("message", "Movie-Theatre can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully movie-theatre created");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{movieTheatreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovieTheatre(int movieTheatreId)
        {
            if (movieTheatreId < 0)
                return BadRequest();

            if (!_movieTheatreRepository.MovieTheatreExists(movieTheatreId))
            {
                ModelState.AddModelError("message", "Movie-Theatre doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieLocation = _movieTheatreRepository.GetMovieTheatre(movieTheatreId);

            if (!_movieTheatreRepository.DeleteMovieTheatre(movieLocation))
            {
                ModelState.AddModelError("message", "Movie-Theatre can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
