namespace WorldExplorer.Web.Services.Country
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetCountryListAsync();
        Task<CountryModel?> GetCountryByNameAsync(string name);
    }
}