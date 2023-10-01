using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DataContext _context;

        public RefreshTokenRepository(DataContext context)
        {
            _context = context;
        }

        public bool UpdateRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);

            return Save();
        }

        public RefreshToken GetRefreshToken(string token)
        {
            return _context.RefreshTokens.First(t => t.Token == token);
        }

        public bool RefreshTokenExists(string token)
        {
            var tokenExists = _context.RefreshTokens.FirstOrDefault(r => r.Token == token);

            if (tokenExists == null)
            {
                return false;
            }

            return true;
        }

        public bool AddRefreshToken(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool DeleteAllRefreshToken(IEnumerable<RefreshToken> tokens)
        {
            _context.RefreshTokens.RemoveRange(tokens);

            return Save();
        }

        public IEnumerable<RefreshToken> GetAllRefreshTokens(int UserId)
        {
            var refreshTokens = _context.RefreshTokens.Where(t => t.UserId == UserId);

            return refreshTokens;
        }
    }
}
