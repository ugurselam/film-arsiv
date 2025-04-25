using System.Threading.Tasks;
using Film_Arsiv.Models;

namespace Film_Arsiv.Services
{
    public interface IFilmService
    {
        Task<List<Film>> SearchFilmsAsync(string query);
        Task<Film?> GetFilmDetailsAsync(string imdbId);
    }
}
