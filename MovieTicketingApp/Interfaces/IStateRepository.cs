using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IStateRepository
    {
        State GetState(int userId);

        bool CreateState(int userId);

        List<string> GetAllowedLanguageCodes();

        Dictionary<string, string> GetTimings();

        bool SetLanguage(string language, int userId);

        bool SetLocation(string location, int userId);

        bool StateExists(int userId);

        bool Save();
    }
}
