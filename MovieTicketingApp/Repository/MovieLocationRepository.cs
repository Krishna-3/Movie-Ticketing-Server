using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class MovieLocationRepository : IMovieLocationRepository
    {
        private DataContext _context;

        public MovieLocationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(MovieLocation movieLocation)
        {
            _context.Add(movieLocation);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
