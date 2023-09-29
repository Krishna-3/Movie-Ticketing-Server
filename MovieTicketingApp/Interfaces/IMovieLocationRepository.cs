using MovieTicketingApp.DTO;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IMovieLocationRepository
    {
        IEnumerable<MovieLocation> GetAllMovieLocations();  

        MovieLocation GetMovieLocation(int id);

        bool CreateMovieLocation(MovieLocation movieLocation);

        bool DeleteMovieLocation(MovieLocation movieLocation);

        bool MovieLocationExists(MovieLocationDto movieLocationDto);

        bool MovieLocationExists(int movieLocationId);

        bool MovieLocationExists(int movieId, int locationId);

        bool Save();
    }
}
