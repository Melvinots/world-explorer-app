namespace WorldExplorer.Web.Integrations.RestCountries
{
    public class CountryApiException : Exception
    {
        public CountryApiException(string message, Exception? inner = null)
            : base(message, inner) { }
    }
}