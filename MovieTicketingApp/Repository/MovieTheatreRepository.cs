using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class MovieTheatreRepository : IMovieTheatreRepository
    {
        private DataContext _context;

        public MovieTheatreRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(MovieTheatre movieTheatre)
        {
            _context.Add(movieTheatre);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
