using MovieTicketingApp.DTO;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ITheatreRepository
    {
        IEnumerable<Theatre> GetTheatresForMovie(int movieId, string city);

        IEnumerable<Theatre> GetAllTheatres();
        
        Theatre GetTheatre(int theatreId);

        bool CreateTheatre(Theatre theatre);

        bool UpdateTheatre(Theatre theatre);

        bool DeleteTheatre(Theatre theatre);

        bool TheatreExists(Theatre theatre);

        bool TheatreExists(TheatreName theatre);

        bool TheatreExists(int theatreId);

        bool Save();
    }
}
