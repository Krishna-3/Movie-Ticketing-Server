using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ITheatreRepository
    {
        bool CreateLocation(Theatre theatre);

        bool Save();
    }
}
