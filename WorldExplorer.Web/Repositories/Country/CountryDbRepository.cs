namespace WorldExplorer.Web.Repositories.Country
{
    public class CountryDbRepository : ICountryRepository
    {
        public Task<List<CountryModel>> GetCountryListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CountryModel?> GetCountryByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}