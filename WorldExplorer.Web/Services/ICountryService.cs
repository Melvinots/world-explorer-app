namespace WorldExplorer.Web.Services
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetCountryListAsync();
        Task<CountryModel?> GetCountryByNameAsync(string name);
    }
}