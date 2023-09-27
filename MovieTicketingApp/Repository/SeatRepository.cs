using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using System.Xml.Serialization;

namespace MovieTicketingApp.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private DataContext _context;

        public SeatRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Seat> GetSeats()
        {
            return _context.Seats.ToList();
        }
    }
}
