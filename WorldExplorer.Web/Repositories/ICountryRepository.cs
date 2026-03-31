namespace WorldExplorer.Web.Repositories
{
    public interface ICountryRepository
    {
        Task<List<CountryModel>> GetCountryListAsync();
        Task<CountryModel?> GetCountryByNameAsync(string name);
    }
}
