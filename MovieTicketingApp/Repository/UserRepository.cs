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
            _context.Add(user);

            return SaveAsync();
        }

        public bool SaveAsync()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
