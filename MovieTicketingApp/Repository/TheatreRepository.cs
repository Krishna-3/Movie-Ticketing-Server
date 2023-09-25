using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class TheatreRepository : ITheatreRepository
    {

        private DataContext _context;

        public TheatreRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(Theatre theatre)
        {
            _context.Add(theatre);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
