using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using WorldExplorer.Domain.Enums;
using WorldExplorer.Web.Repositories.Currency;

namespace WorldExplorer.Web.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMemoryCache _cache;

        public CurrencyService(ICurrencyRepository repo, IMemoryCache cache)
        {
            _currencyRepository = repo;
            _cache = cache;
        }

        public async Task<List<CurrencyModel>> GetCurrenciesAsync()
        {
            if (_cache.TryGetValue(CacheKey.Currencies.ToKeyString(), out List<CurrencyModel>? cached))
                return cached!;

            var currencies = await _currencyRepository.GetCurrenciesAsync();

            _cache.Set(CacheKey.Currencies.ToKeyString(), currencies, TimeSpan.FromHours(1));
            return currencies;
        }

        public Task<ConversionResult?> ConvertAsync(string fromCurrency, string toCurrency, decimal amount = 1)
        {
            return _currencyRepository.ConvertAsync(fromCurrency, toCurrency, amount);
        }

        public Task<HistoricalRatesResult?> GetHistoricalRatesAsync(string fromCurrency, string toCurrency, DateOnly startDate)
        {
            return _currencyRepository.GetHistoricalRatesAsync(fromCurrency, toCurrency, startDate);
        }
    }
}
