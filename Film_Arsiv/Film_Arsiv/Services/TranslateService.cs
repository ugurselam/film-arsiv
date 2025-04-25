using System.Text.Json;
using System.Text;
using System;

namespace Film_Arsiv.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly HttpClient _httpClient;

        public TranslateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TranslateTextAsync(string text)
        {
            var translated = new StringBuilder();
            int maxLength = 499;

            for (int i = 0; i < text.Length; i += maxLength)
            {
                var part = text.Substring(i, Math.Min(maxLength, text.Length - i)); 
                var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(part)}&langpair=en|tr";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    translated.Append(json.RootElement
                                          .GetProperty("responseData")
                                          .GetProperty("translatedText")
                                          .GetString());
                }
            }

            return translated.ToString();
        }
    }
}
