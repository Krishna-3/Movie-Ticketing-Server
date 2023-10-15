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

        public IEnumerable<Ticket> GetBookedTickets(TicketId ticket, DateTime dateTime)
        {
            IEnumerable<Ticket> tickets = _context.Tickets.Where(t => t.Time == dateTime &&
                                                                    t.MovieTheatre.MovieId == ticket.MovieId &&
                                                                    t.MovieTheatre.TheatreId == ticket.TheatreId);
            return tickets;
        }

        public IEnumerable<Ticket> GetTickets(int userId)
        {
            var tickets = _context.Tickets.Where(t => t.UserId == userId && t.Time>= DateTime.Now)
                                          .Include(t => t.MovieTheatre.Movie)
                                          .Include(t => t.MovieTheatre.Theatre)
                                          .Include(t => t.MovieTheatre.Theatre.Location)
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
                                                                    t.MovieTheatre.MovieId == ticket.MovieId&&
                                                                    t.MovieTheatre.TheatreId == ticket.TheatreId&&
                                                                    t.Seat.Id == ticket.SeatId);

            if (ticketExists == null)
            {
                return false;
            }

            return true;
        }
    }
}
