using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieTheatreRepository
    {
        bool CreateLocation(MovieTheatre movieTheatre);

        bool Save();
    }
}
