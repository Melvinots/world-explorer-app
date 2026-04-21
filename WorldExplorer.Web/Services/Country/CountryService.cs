using Microsoft.Extensions.Caching.Memory;
using WorldExplorer.Domain.Enums;
using WorldExplorer.Web.Repositories.Country;

namespace WorldExplorer.Web.Services.Country
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMemoryCache _cache;

        public CountryService(ICountryRepository repo, IMemoryCache cache)
        {
            _countryRepository = repo;
            _cache = cache;
        }

        public async Task<List<CountryModel>> GetCountryListAsync()
        {
            if (_cache.TryGetValue(CacheKey.Countries.ToKeyString(), out List<CountryModel>? cached))
                return cached!;

            var countries = await _countryRepository.GetCountryListAsync();
            var sorted = countries.OrderBy(c => c.Name?.Common).ToList();

            _cache.Set(CacheKey.Countries.ToKeyString(), sorted, TimeSpan.FromHours(1));
            return sorted;
        }

        public async Task<CountryModel?> GetCountryByNameAsync(string name)
        {
            return await _countryRepository.GetCountryByNameAsync(name);
        }
    }
}