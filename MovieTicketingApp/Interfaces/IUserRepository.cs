using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(User user);

        bool SaveAsync();
    }
}
