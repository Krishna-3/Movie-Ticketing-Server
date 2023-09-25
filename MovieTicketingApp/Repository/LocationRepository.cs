using Microsoft.EntityFrameworkCore;
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
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
