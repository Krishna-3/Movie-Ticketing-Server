using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovie(Movie movie)
        {
            _context.Add(movie);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
