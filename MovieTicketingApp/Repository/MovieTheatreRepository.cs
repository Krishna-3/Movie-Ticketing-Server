using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.Data;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class MovieTheatreRepository : IMovieTheatreRepository
    {
        private DataContext _context;

        public MovieTheatreRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovieTheatre(MovieTheatre movieTheatre)
        {
            _context.Add(movieTheatre);

            return Save();
        }

        public bool DeleteMovieTheatre(MovieTheatre movieTheatre)
        {
            _context.Remove(movieTheatre);

            return Save();
        }

        public bool DeleteMovieTheatres(IEnumerable<MovieTheatre> movieTheatres)
        {
            _context.RemoveRange(movieTheatres);

            return Save();
        }

        public IEnumerable<MovieTheatre> GetAllMovieTheatres()
        {
            return _context.MovieTheatres.Include(mt => mt.Movie).Include(mt => mt.Theatre).ToList();
        }

        public MovieTheatre GetMovieTheatre(int id)
        {
            return _context.MovieTheatres.First(mt => mt.Id == id);
        }

        public MovieTheatre GetMovieTheatre(int movieId, int theatreId)
        {
            return _context.MovieTheatres.First(mt => mt.MovieId == movieId && mt.TheatreId==theatreId);
        }

        public IEnumerable<MovieTheatre> GetMovieTheatresForLocation(int locationId)
        {
            var movieTheatres = _context.MovieTheatres.Where(mt => mt.Theatre.LocationId == locationId);

            return movieTheatres;
        }

        public bool MovieTheatreExists(MovieTheatreDto movieTheatreDto)
        {
            var movieLocationExists = _context.MovieTheatres.FirstOrDefault(mt => mt.MovieId == movieTheatreDto.MovieId &&
                                                                                   mt.TheatreId == movieTheatreDto.TheatreId);

            if (movieLocationExists == null)
            {
                return false;
            }

            return true;
        }

        public bool MovieTheatreExists(int movieTheatreId)
        {
            var movieLocationExists = _context.MovieTheatres.FirstOrDefault(mt => mt.Id == movieTheatreId);

            if (movieLocationExists == null)
            {
                return false;
            }

            return true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
