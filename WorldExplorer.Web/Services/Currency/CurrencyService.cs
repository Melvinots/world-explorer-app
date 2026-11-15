using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using WorldExplorer.Domain.Enums;
using WorldExplorer.Web.Integrations.Interfaces;

namespace WorldExplorer.Web.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private static readonly TimeSpan CurrenciesCacheDuration = TimeSpan.FromHours(1);

        private static readonly SemaphoreSlim CurrenciesLock = new(1, 1);

        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMemoryCache _cache;

        public CurrencyService(ICurrencyRepository repo, IMemoryCache cache)
        {
            _currencyRepository = repo ?? throw new ArgumentNullException(nameof(repo));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<List<CurrencyModel>> GetCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            var key = CacheKey.Currencies.ToKeyString();

            if (TryGetCachedCurrencies(key, out var cached))
                return cached;

            await CurrenciesLock.WaitAsync(cancellationToken);
            try
            {
                if (TryGetCachedCurrencies(key, out cached))
                    return cached;

                var currencies = await _currencyRepository.GetCurrenciesAsync(cancellationToken);

                if (currencies is { Count: > 0 })
                {
                    _cache.Set(key, currencies, CurrenciesCacheDuration);
                    return new List<CurrencyModel>(currencies);
                }

                return new List<CurrencyModel>();
            }
            finally
            {
                CurrenciesLock.Release();
            }
        }

        public async Task<ExchangeRateResponse?> ConvertAsync(
            string fromCurrency,
            string toCurrency,
            decimal amount = 1,
            CancellationToken cancellationToken = default)
        {
            var from = NormalizeCode(fromCurrency, nameof(fromCurrency));
            var to = NormalizeCode(toCurrency, nameof(toCurrency));

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount must be greater than zero.");

            return await _currencyRepository.ConvertAsync(from, to, amount, cancellationToken);
        }

        public async Task<HistoricalExchangeRateResponse?> GetHistoricalRatesAsync(
            string fromCurrency,
            string toCurrency,
            DateOnly startDate,
            CancellationToken cancellationToken = default)
        {
            var from = NormalizeCode(fromCurrency, nameof(fromCurrency));
            var to = NormalizeCode(toCurrency, nameof(toCurrency));

            if (startDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentOutOfRangeException(nameof(startDate), startDate, "Start date cannot be in the future.");

            return await _currencyRepository.GetHistoricalRatesAsync(from, to, startDate, cancellationToken);
        }

        private bool TryGetCachedCurrencies(string key, out List<CurrencyModel> copy)
        {
            if (_cache.TryGetValue(key, out List<CurrencyModel>? cached) && cached is not null)
            {
                copy = new List<CurrencyModel>(cached);
                return true;
            }

            copy = new List<CurrencyModel>();
            return false;
        }

        private static string NormalizeCode(string code, string paramName)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Currency code is required.", paramName);

            return code.Trim().ToUpperInvariant();
        }
    }
}
