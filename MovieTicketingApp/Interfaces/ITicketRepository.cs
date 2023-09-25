using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ITicketRepository
    {
        bool CreateLocation(Ticket ticket);

        bool Save();
    }
}
