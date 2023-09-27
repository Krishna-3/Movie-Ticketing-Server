using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ISeatRepository
    {
        IEnumerable<Seat> GetSeats();
    }
}
