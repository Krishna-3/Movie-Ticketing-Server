using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(Location location)
        {
            _context.Add(location);

            return Save();
        }

        public bool DeleteLocation(Location location)
        {
            _context.Remove(location);

            return Save();
        }

        public Location GetLocation(int locationId)
        {
            var location = _context.Locations.First(l => l.Id == locationId);

            return location;
        }

        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        public bool LocationExists(int locationId)
        {
            var locationExists = _context.Locations.FirstOrDefault(l => l.Id == locationId);

            if (locationExists == null)
            {
                return false;
            }

            return true;
        }

        public bool LocationExists(string city)
        {
            var locationExists = _context.Locations.FirstOrDefault(l => l.City == city);

            if (locationExists == null)
            {
                return false;
            }

            return true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateLocation(Location location)
        {
            _context.Update(location);

            return Save();
        }
    }
}
