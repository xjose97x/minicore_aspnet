using System;
using System.Text.Json;
using minicore.Interfaces;
using minicore.Models;
using minicore.Models.LanguageTool;

namespace minicore.Services
{
	public class LanguageService : ILanguageService
	{
        private readonly IHttpClientFactory httpClientFactory;

        public LanguageService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public float FleschKinkaidScore(string text)
        {
            // calculate Flesch-Kinkaid score
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var sentences = text.Split('.', StringSplitOptions.RemoveEmptyEntries);
            var syllables = 0;
            foreach (var word in words)
            {
                syllables += word.Count(c => "aeiouy".Contains(c));
            }
            var score = 206.835f - 1.015f * (words.Length / sentences.Length) - 84.6f * (syllables / words.Length);
            if (score < 0)
            {
                score = 0;
            }
            else if (score > 100)
            {
                score = 100;
            }
            return score;
        }

        public async Task<LanguageToolResult> LanguageTool(string text)
        {

            var httpClient = httpClientFactory.CreateClient();

            // call languagetool check API using httpClient
            var httpResponseMessage = await httpClient.PostAsJsonAsync($"https://languagetool.org/api/v2/check", new
            {
                text = text,
                language = "en-US",
            });

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"LanguageTool API call failed with status code {httpResponseMessage.StatusCode}");
            }

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var languageToolResult = await JsonSerializer.DeserializeAsync<LanguageToolResult>(contentStream);
            return languageToolResult!;
        }
    }
}

