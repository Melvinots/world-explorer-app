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

        /// <summary>
        /// Response from /latest?from=USD&to=PHP
        /// </summary>
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
        /// Response from /2020-01-01..?from=USD&to=PHP (historical time series)
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

            /// <summary>
            /// Key: date string (e.g. "2023-01-01"), Value: dictionary of currency code to rate
            /// </summary>
            [JsonPropertyName("rates")]
            public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; } = new();

            /// <summary>
            /// Flattens the rates into a list of data points for charting.
            /// </summary>
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

        /// <summary>
        /// A single data point for the historical chart.
        /// </summary>
        public class RateDataPoint
        {
            public DateOnly Date { get; set; }
            public decimal Rate { get; set; }
        }
}
