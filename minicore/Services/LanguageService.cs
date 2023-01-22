using System;
using System.Net.Http.Headers;
using System.Text.Json;
using minicore.Interfaces;
using minicore.Models;
using minicore.Models.LanguageTool;
using Newtonsoft.Json;

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

            IEnumerable<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("language", "en-US")
            };

            var content = new FormUrlEncodedContent(data);
            var httpResponseMessage = await httpClient.PostAsync($"https://languagetool.org/api/v2/check", content);
            var bodyMessage = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"LanguageTool API call failed with status code {httpResponseMessage.StatusCode}. Message: {bodyMessage}");
            }

            var rawResult = JsonConvert.DeserializeObject<RawResult>(bodyMessage);

            var languageToolResult = new LanguageToolResult
            {
                Original = text,
                Matches = rawResult.Matches,
                Autofix = string.Empty // todo figure out how to autofix
            };

            return languageToolResult!;
        }
    }
}

