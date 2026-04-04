using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using WorldExplorer.Web.Repositories.Currency;

namespace WorldExplorer.Web.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "currencies";

        public CurrencyService(ICurrencyRepository repo, IMemoryCache cache)
        {
            _currencyRepository = repo;
            _cache = cache;
        }

        public async Task<List<CurrencyModel>> GetCurrenciesAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<CurrencyModel>? cached))
                return cached!;

            var currencies = await _currencyRepository.GetCurrenciesAsync();

            _cache.Set(CacheKey, currencies, TimeSpan.FromHours(1));
            return currencies;
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
