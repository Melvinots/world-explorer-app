using Microsoft.Extensions.Caching.Memory;
using WorldExplorer.Web.Repositories;

namespace WorldExplorer.Web.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repo;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "countries";

        public CountryService(ICountryRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<List<CountryModel>> GetCountryListAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<CountryModel>? cached))
                return cached!;

            var countries = await _repo.GetCountryListAsync();
            var sorted = countries.OrderBy(c => c.Name?.Common).ToList();

            _cache.Set(CacheKey, sorted, TimeSpan.FromHours(1));
            return sorted;
        }

        public async Task<CountryModel?> GetCountryByNameAsync(string name)
        {
            return await _repo.GetCountryByNameAsync(name);
        }
            
    }

}