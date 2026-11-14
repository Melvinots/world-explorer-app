using WorldExplorer.Web.Integrations.RestCountries.Dtos;

namespace WorldExplorer.Web.Repositories.Country
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Fetches all available countries from RestCountries.
        /// Use this to populate dropdowns.
        /// GET /all?fields=name,cca2,capital,region,population
        /// </summary>
        Task<List<CountryModel>> GetCountryListAsync(CancellationToken ct = default);

        /// <summary>
        /// Fetches a single country by name.
        /// GET /name/{name}
        /// </summary>
        //Task<CountryModel?> GetCountryByNameAsync(string name);
    }
}
