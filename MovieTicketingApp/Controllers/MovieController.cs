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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IStateRepository _state;

        public MovieController(IMovieRepository movieRepository, IMapper mapper, IStateRepository state)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _state = state;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieEnDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieTeDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieHiDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovies()
        {
            if (_state.GetLocation().IsNullOrEmpty())
            {
                ModelState.AddModelError("message", "Please select a location");
                return BadRequest(ModelState);
            }

            var city = _state.GetLocation();
            var movies =_movieRepository.GetMovies(city);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var language = _state.GetLanguage();

            if (language == "te")
            {
                var teMovies = _mapper.Map<List<MovieTeDto>>(movies);

                return Ok(teMovies);
            }
            else if (language == "hi")
            {
                var hiMovies = _mapper.Map<List<MovieHiDto>>(movies);

                return Ok(hiMovies);
            }
            else
            {
                var enMovies = _mapper.Map<List<MovieEnDto>>(movies);

                return Ok(enMovies);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
                return BadRequest();

            if (_movieRepository.MovieExists(movie))
            {
                ModelState.AddModelError("message", "Movie already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_movieRepository.CreateMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully movie created");
        }

        [HttpPatch("title/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie(int movieId, [FromBody] MovieTitle Title)
        {
            if (movieId < 0)
                return BadRequest();

            if (Title == null)
                return BadRequest();

            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovie(movieId);
            movie.TitleTe = Title.TitleTe;
            movie.TitleHi = Title.TitleHi;
            movie.TitleEn = Title.TitleEn;

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("description/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDescription(int movieId, [FromBody] MovieDescription Description)
        {
            if (movieId < 0)
                return BadRequest();

            if (Description == null)
                return BadRequest();

            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovie(movieId);
            movie.DescriptionTe = Description.DescriptionTe;
            movie.DescriptionHi = Description.DescriptionHi;
            movie.DescriptionEn = Description.DescriptionEn;

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (movieId < 0)
                return BadRequest();

            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovie(movieId);

            if (!_movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}