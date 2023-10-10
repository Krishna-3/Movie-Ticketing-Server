using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using System.Security.Claims;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IStateRepository _stateRepository;

        public MovieController(IMovieRepository movieRepository, IMapper mapper, IStateRepository stateRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _stateRepository = stateRepository;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieEnDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieTeDto>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieHiDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovies()
        {
            int userId = Int32.Parse(HttpContext.User.FindFirstValue("Id"));

            if (!_stateRepository.StateExists(userId) || _stateRepository.GetState(userId).selectedLocation.IsNullOrEmpty())
            {
                ModelState.AddModelError("message", "Please select a location");
                return BadRequest(ModelState);
            }

            var city = _stateRepository.GetState(userId).selectedLocation;
            var movies =_movieRepository.GetMovies(city);

            foreach(var movie in movies)
            {
                movie.Photo = GetImage(Convert.ToBase64String(movie.Photo));
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var language = _stateRepository.GetState(userId).preferredLanguage;

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
        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(MovieEnDto))]
        [ProducesResponseType(200, Type = typeof(MovieTeDto))]
        [ProducesResponseType(200, Type = typeof(MovieHiDto))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieById(int movieId)
        {
            int userId = Int32.Parse(HttpContext.User.FindFirstValue("Id"));
            var movie = _movieRepository.GetMovie(movieId);

            if (movie == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var language = _stateRepository.GetState(userId).preferredLanguage;

            movie.Photo = GetImage(Convert.ToBase64String(movie.Photo));

            if (language == "te")
            {
                var teMovie = _mapper.Map<MovieTeDto>(movie);

                return Ok(teMovie);
            }
            else if (language == "hi")
            {
                var hiMovie = _mapper.Map<MovieHiDto>(movie);

                return Ok(hiMovie);
            }
            else
            {
                var enMovie = _mapper.Map<MovieEnDto>(movie);

                return Ok(enMovie);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllMovies()
        {
            var movies = _movieRepository.GetAllMovies();

            return Ok(movies);
        }

        [Authorize(Roles = "admin")]
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
            
            movie.Photo = new MemoryStream().ToArray();

            if (!_movieRepository.CreateMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok(new Response()
            {
                Success= "Successfully movie created"
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("title/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovieTitle(int movieId, [FromBody] MovieTitle Title)
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

        [Authorize(Roles = "admin")]
        [HttpPatch("description/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovieDescription(int movieId, [FromBody] MovieDescription Description)
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

        [Authorize(Roles = "admin")]
        [HttpPatch("language/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovieLanguage(int movieId, [FromBody] MovieLanguage Language)
        {
            if (movieId < 0)
                return BadRequest();

            if (Language == null)
                return BadRequest();

            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovie(movieId);
            movie.LanguageTe = Language.LanguageTe;
            movie.LanguageHi = Language.LanguageHi;
            movie.LanguageEn = Language.LanguageEn;

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("rating/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovieRating(int movieId, [FromBody] float rating)
        {
            if (movieId < 0)
                return BadRequest();

            if (rating < 1 || rating >5)
                return BadRequest();

            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovie(movieId);
            movie.Rating = rating;

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("message", "Movie can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost("photo/{movieId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UploadPhoto(int movieId, [FromForm] IFormFile photo)
        {
            if (movieId < 0 || photo == null)
                return BadRequest();

            if (photo.ContentType != "image/jpeg" || photo.Length > 2*1000*1000)
            {
                ModelState.AddModelError("message", "image should follow constraints");
                return BadRequest(ModelState);
            }

            if (!_movieRepository.MovieExists(movieId))
            {
                ModelState.AddModelError("message", "Movie doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _movieRepository.GetMovie(movieId);
            if (photo.Length > 0)
             {
                 using (var ms = new MemoryStream())
                 {
                     photo.CopyTo(ms);
                     var fileBytes = ms.ToArray();
                     movie.Photo = fileBytes;
                 }
             }

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("message", "Photo can not be uploaded");
                return StatusCode(500, ModelState);
            }

            return Ok(new Response()
            {
                Success= "Photo Uploaded successfully"
            });
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("photo/{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePhoto(int movieId)
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
            movie.Photo = new MemoryStream().ToArray();

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("message", "Photo can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}