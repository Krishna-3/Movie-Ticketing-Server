using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface ISeatRepository
    {
        bool CreateLocation(Seat seat);

        bool Save();
    }
}
