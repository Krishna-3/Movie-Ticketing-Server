using MovieTicketingApp.DTO;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieTheatreRepository
    {
        IEnumerable<MovieTheatre> GetAllMovieTheatres();

        MovieTheatre GetMovieTheatre(int id);

        bool CreateMovieTheatre(MovieTheatre movieTheatre);

        bool DeleteMovieTheatre(MovieTheatre movieTheatre);

        bool MovieTheatreExists(MovieTheatreDto movieTheatreDto);

        bool MovieTheatreExists(int movieTheatreId);

        bool Save();
    }
}
