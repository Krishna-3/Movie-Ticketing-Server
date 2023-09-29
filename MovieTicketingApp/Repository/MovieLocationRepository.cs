using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class MovieLocationRepository : IMovieLocationRepository
    {
        private DataContext _context;

        public MovieLocationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovieLocation(MovieLocation movieLocation)
        {
            _context.Add(movieLocation);

            return Save();
        }

        public bool DeleteMovieLocation(MovieLocation movieLocation)
        {
            _context.Remove(movieLocation);

            return Save();
        }

        public IEnumerable<MovieLocation> GetAllMovieLocations()
        {
            return _context.MovieLocations.Include(ml => ml.Movie).Include(ml => ml.Location).ToList();
        }

        public MovieLocation GetMovieLocation(int id)
        {
            return _context.MovieLocations.First(ml => ml.Id == id);
        }

        public bool MovieLocationExists(MovieLocationDto movieLocationDto)
        {
            var movieLocationExists = _context.MovieLocations.FirstOrDefault(ml => ml.MovieId == movieLocationDto.MovieId &&
                                                                                   ml.LocationId == movieLocationDto.LocationId);

            if (movieLocationExists == null)
            {
                return false;
            }

            return true;
        }

        public bool MovieLocationExists(int movieLocationId)
        {
            var movieLocationExists = _context.MovieLocations.FirstOrDefault(ml => ml.Id == movieLocationId);

            if (movieLocationExists == null)
            {
                return false;
            }

            return true;
        }

        public bool MovieLocationExists(int movieId, int locationId)
        {
            var movieLocationExists = _context.MovieLocations.FirstOrDefault(ml => ml.MovieId == movieId && ml.LocationId == locationId);

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
