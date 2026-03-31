namespace WorldExplorer.Web.Repositories
{
    public class CountryApiRepository : ICountryRepository
    {
        private readonly HttpClient _http;

        public CountryApiRepository(HttpClient http) => _http = http;

        public async Task<List<CountryModel>> GetCountryListAsync()
        {
            return await _http.GetFromJsonAsync<List<CountryModel>>(
                "all?fields=name,cca2,capital,region,population"
            ) ?? new();
        }

        public async Task<CountryModel?> GetCountryByNameAsync(string name)
        {
            var countries = await _http.GetFromJsonAsync<List<CountryModel>>($"name/{name}");
            return countries?.FirstOrDefault();
        }
    }
}
