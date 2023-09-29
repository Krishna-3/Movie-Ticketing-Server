using MovieTicketingApp.DTO;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetTickets(int userId);

        bool CreateTicket(Ticket ticket);

        bool DeleteTicket(Ticket ticket);

        bool TicketExists(TicketId ticket, DateTime dateTime);

        bool Save();
    }
}
