using System.Net;
using System.Text.Json;
using WorldExplorer.Web.Integrations.Interfaces;
using WorldExplorer.Web.Integrations.RestCountries.Dtos;
using WorldExplorer.Web.Integrations.RestCountries.Mapping;

namespace WorldExplorer.Web.Integrations.RestCountries
{
    public class RestCountriesRepository : ICountryRepository
    {
        private readonly HttpClient _http;
        private readonly ILogger<RestCountriesRepository> _logger;

        private const int PageSize = 100;

        public RestCountriesRepository(HttpClient http, ILogger<RestCountriesRepository> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<CountryModel>> GetCountryListAsync(CancellationToken ct = default)
        {
            var dtos = new List<CountryDto>();
            var offset = 0;

            try
            {
                while (true)
                {
                    var page = await _http.GetFromJsonAsync<CountriesResponse>(
                        $"?limit={PageSize}&offset={offset}", ct);

                    if (page?.Data is null) break;

                    dtos.AddRange(page.Data.Objects);

                    if (!page.Data.Meta.More) break;
                    offset += PageSize;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "REST Countries list request failed at offset {Offset} (status {Status})",
                    offset, ex.StatusCode);
                throw new CountryApiException("Country data is temporarily unavailable.", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Unexpected response shape from REST Countries at offset {Offset}", offset);
                throw new CountryApiException("Received invalid data from the country service.", ex);
            }

            return dtos.Select(CountryMapper.ToModel).ToList();
        }

        public async Task<CountryModel?> GetCountryByNameAsync(string name, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var url = $"names.common?q={Uri.EscapeDataString(name.Trim())}&limit=10";

            try
            {
                var response = await _http.GetFromJsonAsync<CountriesResponse>(url, ct);
                var objects = response?.Data?.Objects ?? new();

                var dto = objects.FirstOrDefault(c =>
                    string.Equals(c.Names.Common, name.Trim(), StringComparison.OrdinalIgnoreCase));

                return dto is null ? null : CountryMapper.ToModel(dto);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "REST Countries lookup failed for {Name} (status {Status})", name, ex.StatusCode);
                throw new CountryApiException("Country data is temporarily unavailable.", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Unexpected response shape for lookup of {Name}", name);
                throw new CountryApiException("Received invalid data from the country service.", ex);
            }
        }
    }
}