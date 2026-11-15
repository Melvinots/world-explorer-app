using System.Text.Json.Serialization;

namespace WorldExplorer.Web.Integrations.Frankfurter.Dtos
{
    public class HistoricalExchangeRateResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("rates")]
        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; } = new();

        public List<RateDataPoint> ToDataPoints(string targetCurrency)
        {
            return Rates
                .Where(r => r.Value.ContainsKey(targetCurrency))
                .Select(r => new RateDataPoint
                {
                    Date = DateOnly.Parse(r.Key),
                    Rate = r.Value[targetCurrency]
                })
                .OrderBy(r => r.Date)
                .ToList();
        }
    }
}
