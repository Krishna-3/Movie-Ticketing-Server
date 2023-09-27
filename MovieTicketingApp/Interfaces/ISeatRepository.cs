using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ISeatRepository
    {
        bool CreateSeats();

        IEnumerable<Seat> GetSeats();
    }
}
