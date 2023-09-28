using MovieTicketingApp.Models;

namespace MovieTicketingApp.Interfaces
{
    public interface IStateRepository
    {
        string GetLanguage();

        string GetLocation();

        List<string> GetAllowedLanguageCodes();

        void SetLanguage(string language);

        void SetLocation(string location);
    }
}
