using MovieTicketingApp.DTO;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieTheatreRepository
    {
        IEnumerable<MovieTheatre> GetAllMovieTheatres();

        IEnumerable<MovieTheatre> GetMovieTheatresForLocation(int locationId);

        MovieTheatre GetMovieTheatre(int id);

        bool CreateMovieTheatre(MovieTheatre movieTheatre);

        bool DeleteMovieTheatre(MovieTheatre movieTheatre);

        bool DeleteMovieTheatres(IEnumerable<MovieTheatre> movieTheatre);

        bool MovieTheatreExists(MovieTheatreDto movieTheatreDto);

        bool MovieTheatreExists(int movieTheatreId);

        int GetMovieTheatreId(int movieId, int theatreId);

        bool Save();
    }
}
