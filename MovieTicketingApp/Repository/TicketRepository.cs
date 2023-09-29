using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.DTO;
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

        public bool CreateTicket(Ticket ticket)
        {
            _context.Add(ticket);

            return Save();
        }

        public bool DeleteTicket(Ticket ticket)
        {
            _context.Remove(ticket);

            return Save();
        }

        public IEnumerable<Ticket> GetTickets(int userId)
        {
            var tickets = _context.Tickets.Where(t => t.UserId == userId)
                                          .Include(t => t.MovieTheatre)
                                          .Include(t => t.Seat);

            return tickets;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool TicketExists(TicketId ticket, DateTime dateTime)
        {
            var ticketExists = _context.Tickets.FirstOrDefault(t => t.Time == dateTime &&
                                                                    t.MovieTheatre.Id == ticket.MovieTheatreId &&
                                                                    t.Seat.Id == ticket.SeatId);

            if (ticketExists == null)
            {
                return false;
            }

            return true;
        }
    }
}
