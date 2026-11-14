using WorldExplorer.Web.Integrations.RestCountries.Dtos;
using WorldExplorer.Web.Integrations.RestCountries.Mapping;
using WorldExplorer.Web.Repositories.Country;

namespace WorldExplorer.Web.Integrations.RestCountries
{
    public class RestCountriesRepository : ICountryRepository
    {
        private readonly HttpClient _http;

        public RestCountriesRepository(HttpClient http) =>  _http = http;

        public async Task<List<CountryModel>> GetCountryListAsync(CancellationToken ct = default)
        {
            var dtos = new List<CountryDto>();
            var offset = 0;
            int PageSize = 100;

            while (true)
            {
                var page = await _http.GetFromJsonAsync<CountriesResponse>(
                    $"?limit={PageSize}&offset={offset}", ct);

                if (page is null) break;

                dtos.AddRange(page.Data.Objects);

                if (!page.Data.Meta.More) break;
                offset += PageSize;
            }

            return dtos.Select(CountryMapper.ToModel).ToList();
        }

        //public async Task<CountriesResponse> GetCountryByNameAsync(string name)
        //{
        //    var countries = await _http.GetFromJsonAsync<CountriesResponse>($"name/{name}");
        //    return countries?.Data?.FirstOrDefault() ?? new();
        //}
    }
}
