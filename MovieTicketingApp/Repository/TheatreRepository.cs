using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.DTO;
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

        public bool CreateTheatre(Theatre theatre)
        {
            _context.Add(theatre);

            return Save();
        }

        public bool DeleteTheatre(Theatre theatre)
        {
            _context.Remove(theatre);

            return Save();
        }

        public IEnumerable<Theatre> GetAllTheatres()
        {
            return _context.Theatres.Include(t => t.Location);
        }

        public Theatre GetTheatre(int theatreId)
        {
            var theatre = _context.Theatres.Include(t => t.Location)
                                           .FirstOrDefault(t =>  t.Id == theatreId);

            return theatre;
        }

        public IEnumerable<Theatre> GetTheatresForMovie(int movieId, string city)
        {
            var theatres = _context.MovieTheatres.Where(mt => mt.MovieId == movieId && mt.Theatre.Location.City == city)
                                                 .Select(mt => mt.Theatre)
                                                 .Include(t => t.Location);

            return theatres;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool TheatreExists(Theatre theatre)
        {
            var theatreExists = _context.Theatres.FirstOrDefault(t => t == theatre);

            if (theatreExists == null)
            {
                return false;
            }

            return true;
        }

        public bool TheatreExists(int theatreId)
        {
            var theatreExists = _context.Theatres.FirstOrDefault(t => t.Id == theatreId);

            if (theatreExists == null)
            {
                return false;
            }

            return true;
        }

        public bool TheatreExists(TheatreName theatre)
        {
            var theatreExists = _context.Theatres.FirstOrDefault(t => t.NameEn == theatre.NameEn || 
                                                                      t.NameTe == theatre.NameTe ||
                                                                      t.NameHi == theatre.NameHi);

            if (theatreExists == null)
            {
                return false;
            }

            return true;
        }

        public bool UpdateTheatre(Theatre theatre)
        {
            _context.Update(theatre);

            return Save();
        }
    }
}
