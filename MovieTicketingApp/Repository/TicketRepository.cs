using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLocation(Ticket ticket)
        {
            _context.Add(ticket);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
