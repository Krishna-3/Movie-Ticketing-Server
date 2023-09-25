using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ILocationRepository
    {
        bool CreateLocation(Location location);

        bool Save();
    }
}
