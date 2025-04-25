using Film_Arsiv.Models;

namespace Film_Arsiv.Services
{
    public interface ITranslateService
    {
        Task<string> TranslateTextAsync(string text);
    }
}
