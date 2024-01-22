using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureSpellCheckDemo.Util
{
    public static class BingSpellCheck
    {

        public static async Task<SpellCheckResponse?> RunSpellCheck(string? inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                return null;
            }
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("AZURE_BING_SEARCH_SERVICE_KEY", EnvironmentVariableTarget.Machine));

            // Request parameters
            queryString["mkt"] = "en-US";
            queryString["mode"] = "proof";
            queryString["text"] = inputText;

            var uri = "https://api.bing.microsoft.com/v7.0/spellcheck?" + queryString;

            var response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var spellCheckResponse = JsonSerializer.Deserialize<SpellCheckResponse>(responseContent, options);
            if (spellCheckResponse == null)
            {
                return await Task.FromResult<SpellCheckResponse>(null);
            }

            foreach (var flaggedToken in spellCheckResponse.FlaggedTokens)
            {
                Console.WriteLine($"Suggestion for '{flaggedToken.Token}': {flaggedToken.Suggestions[0].SuggestionV2}");
            }

            return spellCheckResponse;
        }



        public class SpellCheckResponse
        {
            public FlaggedToken[] FlaggedTokens { get; set; }
        }

        public class FlaggedToken
        {
            public string Token { get; set; }
            public int Offset { get; set; }
            public string Type { get; set; }
            public Suggestion[] Suggestions { get; set; }
        }

        public class Suggestion
        {
            [JsonPropertyName(name: "Suggestion")]
            public string? SuggestionV2 { get; set; }

            public double Score { get; set; }
        }

    }


}
