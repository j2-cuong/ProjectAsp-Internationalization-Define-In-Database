
using MultiLanguageApp.Models;

namespace MultiLanguageApp
{
    public interface IDatabaseLocalizer
    {
        Task<string> GetTranslation(string key);
        Task<List<LanguageConfig>> GetLanguageConfig();
    }
}