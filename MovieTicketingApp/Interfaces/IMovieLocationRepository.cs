using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieLocationRepository
    {
        bool CreateLocation(MovieLocation movieLocation);

        bool Save();
    }
}
