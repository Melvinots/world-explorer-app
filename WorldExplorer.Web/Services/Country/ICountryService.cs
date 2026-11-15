using WorldExplorer.Web.Integrations.RestCountries.Dtos;

namespace WorldExplorer.Web.Services.Country
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetCountryListAsync(CancellationToken ct);
        Task<CountryModel?> GetCountryByNameAsync(string name, CancellationToken ct);
    }
}