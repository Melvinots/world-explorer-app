using System.Text.Json.Serialization;

namespace WorldExplorer.Web.Integrations.Frankfurter.Dtos
{
    public class ExchangeRateResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; } = [];

        public decimal ConvertedAmount => Rates.Values.FirstOrDefault();
    }
}
