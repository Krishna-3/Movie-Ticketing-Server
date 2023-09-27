using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ILocationRepository
    {
        bool CreateLocation(Location location);

        IEnumerable<Location> GetLocations();   

        bool UpdateLocation(Location location);

        bool DeleteLocation(Location location);

        bool LocationExists(int locationId);

        bool Save();
    }
}
