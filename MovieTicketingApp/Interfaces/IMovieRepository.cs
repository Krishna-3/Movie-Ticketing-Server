using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies(string city);

        Movie GetMovie(int movieId);

        bool CreateMovie(Movie movie);

        bool UpdateMovie(Movie movie);

        bool DeleteMovie(Movie movie);

        bool MovieExists(Movie movie);

        bool MovieExists(int movieId);

        bool MovieExists(string movieName);
        
        bool Save();
    }
}
