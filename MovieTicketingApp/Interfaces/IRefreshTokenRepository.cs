using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IRefreshTokenRepository
    {
        bool RefreshTokenExists(string token);

        RefreshToken GetRefreshToken(string token);

        IEnumerable<RefreshToken> GetAllRefreshTokens(int UserId);

        bool AddRefreshToken(RefreshToken token);

        bool UpdateRefreshToken(RefreshToken token);

        bool DeleteAllRefreshToken(IEnumerable<RefreshToken> tokens);

        bool Save();
    }
}
