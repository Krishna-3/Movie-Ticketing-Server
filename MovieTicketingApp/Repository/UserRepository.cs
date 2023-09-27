using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;                                
        }

        public bool CreateUser(User user)
        {
            user.Role = "user";

            _context.Add(user);

            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);

            return Save();
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.First(u => u.Id == userId);

            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateUser(User user)
        {
            //var user = _context.Users.Find(updatedUser.Id);
            //user.Username = updatedUser.Username;
            // user.Email = updatedUser.Email;

            _context.Update(user);

            return Save();
        }

        public bool UserExists(User user)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            var mailExists = _context.Users.FirstOrDefault(m => m.Email == user.Email);  
            
            if (userExists == null || mailExists == null) 
                return false;

            return true;
        }

        public bool UserExists(int userId)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (userExists == null)
                return false;
            return true;
        }

        public bool UserExists(string username)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Username == username);

            if (userExists == null)
                return false;
            return true;
        }
    }
}
