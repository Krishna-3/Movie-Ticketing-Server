using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovie(Movie movie)
        {
            _context.Add(movie);

            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Remove(movie);

            return Save();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies;
        }

        public Movie GetMovie(int movieId)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);

            return movie;
        }

        public IEnumerable<Movie> GetMovies(string city)
        {
            var movies = _context.MovieLocations.Where(ml => ml.Location.City == city)
                                                .Select(ml => ml.Movie);

            return movies;
        }

        public bool MovieExists(Movie movie)
        {
            var movieExists = _context.Movies.FirstOrDefault(m => m == movie);

            if (movieExists == null)
            {
                return false;
            }

            return true;
        }

        public bool MovieExists(int movieId)
        {
            var movieExists = _context.Movies.FirstOrDefault(m => m.Id == movieId);

            if (movieExists == null)
            {
                return false;
            }

            return true;
        }

        public bool MovieExists(string movieName)
        {
            var movieNameExists = _context.Movies.FirstOrDefault(m => m.TitleEn == movieName ||
                                                                 m.TitleHi == movieName || 
                                                                 m.TitleTe == movieName);

            if (movieNameExists == null)
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

        public bool UpdateMovie(Movie movie)
        {
            
            _context.Update(movie);

            return Save();
        }
    }
}
