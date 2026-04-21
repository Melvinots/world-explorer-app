using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorldExplorer.Domain.Models
{
    public class CurrencyModel
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public override string ToString() => $"{Code} - {Name}";
    }

    public class ConversionResult
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; } = new();

        public decimal ConvertedAmount => Rates.Values.FirstOrDefault();
    }

    /// <summary>
    /// Maps the historical time series response from the Frankfurter API.
    /// Endpoint: /2020-01-01..2024-01-01?from=USD&to=PHP
    /// </summary>
    public class HistoricalRatesResult
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

    public class RateDataPoint
    {
        public DateOnly Date { get; set; }
        public decimal Rate { get; set; }
    }

    public class CurrencyPair
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
}
