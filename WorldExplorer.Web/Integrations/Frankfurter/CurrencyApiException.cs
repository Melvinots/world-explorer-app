namespace WorldExplorer.Web.Integrations.Frankfurter
{
    public class CurrencyApiException : Exception
    {
        public CurrencyApiException(string message, Exception? inner = null)
            : base(message, inner) { }
    }
}
