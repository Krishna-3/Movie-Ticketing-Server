using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieRepository
    {
        bool CreateMovie(Movie movie);

        bool Save();
    }
}
