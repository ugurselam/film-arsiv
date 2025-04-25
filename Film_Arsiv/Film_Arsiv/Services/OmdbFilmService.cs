using System.Net.Http;
using System.Text.Json;
using Film_Arsiv.Models;

namespace Film_Arsiv.Services
{
    public class OmdbFilmService : IFilmService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "d0fb7041"; // 🔑 BURAYA kendi API key'ini yaz

        public OmdbFilmService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. Film araması (listeleme)
        public async Task<List<Film>> SearchFilmsAsync(string query)
        {
            var url = $"http://www.omdbapi.com/?s={query}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<Film>();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var films = new List<Film>();

            if (doc.RootElement.TryGetProperty("Search", out JsonElement results))
            {
                foreach (var item in results.EnumerateArray())
                {
                  //  Film film = GetFilmDetailsAsync(item.GetProperty("imdbID").GetString());
                    films.Add(new Film
                    {
                        Title = item.GetProperty("Title").GetString(),
                        Year = item.GetProperty("Year").GetString(),
                        imdbID = item.GetProperty("imdbID").GetString(),
                        Poster = item.GetProperty("Poster").GetString()
                    });
                }
            }

            return films;
        }

        // 2. Seçilen film detayını al
        public async Task<Film?> GetFilmDetailsAsync(string imdbId)
        {
            var url = $"http://www.omdbapi.com/?i={imdbId}&plot=full&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            var root = doc.RootElement;

            try
            {
                return new Film
                {
                    Title = root.GetProperty("Title").GetString(),
                    Year = root.GetProperty("Year").GetString(),
                    Plot = root.GetProperty("Plot").GetString(),
                    Poster = root.GetProperty("Poster").GetString(),
                    imdbID = root.GetProperty("imdbID").GetString(),
                    Runtime = root.GetProperty("Runtime").GetString(),
                    Genre = root.GetProperty("Genre").GetString(),
                    Director = root.GetProperty("Director").GetString(),
                    Writer = root.GetProperty("Writer").GetString(),
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
