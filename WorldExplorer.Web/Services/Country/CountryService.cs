using Microsoft.Extensions.Caching.Memory;
using WorldExplorer.Domain.Enums;
using WorldExplorer.Web.Integrations.Interfaces;
using WorldExplorer.Web.Integrations.RestCountries.Dtos;
using WorldExplorer.Web.Pages;

namespace WorldExplorer.Web.Services.Country
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _cache;

        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        public CountryService(ICountryRepository repo, IMemoryCache cache)
        {
            _countryRepository = repo;
            _cache = cache;
        }

        public async Task<List<CountryModel>> GetCountryListAsync(CancellationToken ct = default)
        {
            var key = CacheKey.Countries.ToKeyString();

            if (_cache.TryGetValue(key, out List<CountryModel>? cached) && cached is not null)
                return cached;

            var countries = await _countryRepository.GetCountryListAsync(ct);
            var sorted = countries.OrderBy(c => c.Names.Common).ToList();

            if (sorted is { Count: > 0 })
            {
                _cache.Set(key, sorted, CacheDuration);
                return sorted;
            }

            return sorted;
        }

        public async Task<CountryModel?> GetCountryByNameAsync(string name, CancellationToken ct = default)
        {
            if (_cache.TryGetValue(CacheKey.Countries.ToKeyString(), out List<CountryModel>? cached) && cached is not null)
            {
                return cached.FirstOrDefault(c =>
                    c.Names.Common.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            return await _countryRepository.GetCountryByNameAsync(name, ct);
        }
    }
}