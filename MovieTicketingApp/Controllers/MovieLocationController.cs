using AutoMapper;
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

        public MovieLocationController(IMovieLocationRepository movieLocationRepository, IMapper mapper)
        {
            _movieLocationRepository = movieLocationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllMovieLocations() 
        {
            var movieLocations = _movieLocationRepository.GetAllMovieLocations();

            return Ok(movieLocations);
        }

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

            if (!_movieLocationRepository.DeleteMovieLocation(movieLocation))
            {
                ModelState.AddModelError("message", "Movie-Location can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}