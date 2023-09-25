using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private DataContext _context;

        public SeatRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(Seat seat)
        {
            _context.Add(seat);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
