using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IUserRepository
    {
        bool UserExists(User user);

        bool UserExists(int userId);

        bool UserExists(string username);

        User GetUser(int userId);

        User GetUser(string username);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool Save();
    }
}
