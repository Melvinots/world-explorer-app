using static System.Net.WebRequestMethods;

namespace WorldExplorer.Web.Repositories.Currency
{
    public class CurrencyApiRepository : ICurrencyRepository
    {
        private readonly HttpClient _http;

        public CurrencyApiRepository(HttpClient http) => _http = http;

        public async Task<List<CurrencyModel>> GetCurrenciesAsync()
        {
            var currencies = await _http.GetFromJsonAsync<Dictionary<string, string>>(
                "currencies"
            ) ?? new();

            return currencies
                .Select(kvp => new CurrencyModel { Code = kvp.Key, Name = kvp.Value })
                .OrderBy(c => c.Code)
                .ToList();
        }

        public Task<ConversionResult?> ConvertAsync(string fromCurrency, string toCurrency, decimal amount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<HistoricalRatesResult?> GetHistoricalRatesAsync(string fromCurrency, string toCurrency, DateOnly startDate)
        {
            throw new NotImplementedException();
        }
    }
}
