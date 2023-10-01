using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieLocationController : ControllerBase
    {
        private readonly IMovieLocationRepository _movieLocationRepository;
        private readonly IMapper _mapper;
        private readonly IMovieTheatreRepository _movieTheatreRepository;

        public MovieLocationController(IMovieLocationRepository movieLocationRepository, IMapper mapper, IMovieTheatreRepository movieTheatreRepository)
        {
            _movieLocationRepository = movieLocationRepository;
            _mapper = mapper;
            _movieTheatreRepository = movieTheatreRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllMovieLocations() 
        {
            var movieLocations = _movieLocationRepository.GetAllMovieLocations();

            return Ok(movieLocations);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovieLocation(MovieLocationDto movieLocationDto) 
        {
            if (movieLocationDto == null)
                return BadRequest();

            if (_movieLocationRepository.MovieLocationExists(movieLocationDto))
            {
                ModelState.AddModelError("message", "Movie-Location already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieLocation = _mapper.Map<MovieLocation>(movieLocationDto);

            if (!_movieLocationRepository.CreateMovieLocation(movieLocation))
            {
                ModelState.AddModelError("message", "Movie-Location can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully movie-location created");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{movieLocationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovieLocation(int movieLocationId)
        {
            if (movieLocationId < 0)
                return BadRequest();

            if (!_movieLocationRepository.MovieLocationExists(movieLocationId))
            {
                ModelState.AddModelError("message", "Movie-Location doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieLocation = _movieLocationRepository.GetMovieLocation(movieLocationId);
            var movieTheatres = _movieTheatreRepository.GetMovieTheatresForLocation(movieLocation.LocationId);

            if (movieTheatres != null)
            {
                _movieTheatreRepository.DeleteMovieTheatres(movieTheatres);
            }

            if (!_movieLocationRepository.DeleteMovieLocation(movieLocation))
            {
                ModelState.AddModelError("message", "Movie-Location can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}