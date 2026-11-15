using System.Globalization;
using System.Net;
using WorldExplorer.Web.Integrations.Frankfurter.Dtos;
using WorldExplorer.Web.Integrations.Interfaces;

namespace WorldExplorer.Web.Integrations.Frankfurter
{
    public class FrankfurterRepository : ICurrencyRepository
    {
        private readonly HttpClient _http;

        public FrankfurterRepository(HttpClient http) =>
            _http = http ?? throw new ArgumentNullException(nameof(http));

        public async Task<List<CurrencyModel>> GetCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            var currencies = await GetAsync<CurrencyResponse>("currencies", cancellationToken) ?? new();

            return currencies
                .Select(kvp => new CurrencyModel { Code = kvp.Key, Name = kvp.Value })
                .OrderBy(c => c.Code)
                .ToList();
        }

        public Task<ExchangeRateResponse?> ConvertAsync(
            string fromCurrency,
            string toCurrency,
            decimal amount = 1,
            CancellationToken cancellationToken = default)
        {
            var from = EscapeCode(fromCurrency, nameof(fromCurrency));
            var to = EscapeCode(toCurrency, nameof(toCurrency));

            var amountText = amount.ToString(CultureInfo.InvariantCulture);

            return GetAsync<ExchangeRateResponse>(
                $"latest?from={from}&to={to}&amount={amountText}",
                cancellationToken);
        }

        public Task<HistoricalExchangeRateResponse?> GetHistoricalRatesAsync(
            string fromCurrency,
            string toCurrency,
            DateOnly startDate,
            CancellationToken cancellationToken = default)
        {
            var from = EscapeCode(fromCurrency, nameof(fromCurrency));
            var to = EscapeCode(toCurrency, nameof(toCurrency));
            var start = startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            return GetAsync<HistoricalExchangeRateResponse>(
                $"{start}..?from={from}&to={to}",
                cancellationToken);
        }

        private async Task<T?> GetAsync<T>(string relativeUri, CancellationToken cancellationToken) where T : class
        {
            using var response = await _http.GetAsync(
                relativeUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }

        private static string EscapeCode(string code, string paramName)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Currency code is required.", paramName);

            return Uri.EscapeDataString(code.Trim().ToUpperInvariant());
        }
    }
}
