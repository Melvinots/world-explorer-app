namespace WorldExplorer.Web.Repositories.Currency
{
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Fetches all available currencies from Frankfurter.
        /// Use this to populate dropdowns.
        /// GET /currencies
        /// </summary>
        Task<List<CurrencyModel>> GetCurrenciesAsync();

        /// <summary>
        /// Fetches the latest conversion rate between two currencies.
        /// GET /latest?from={from}&to={to}&amount={amount}
        /// </summary>
        Task<ConversionResult?> ConvertAsync(string fromCurrency, string toCurrency, decimal amount = 1);

        /// <summary>
        /// Fetches historical rates between two currencies for charting.
        /// GET /{startDate}..?from={from}&to={to}
        /// </summary>
        Task<HistoricalRatesResult?> GetHistoricalRatesAsync(string fromCurrency, string toCurrency, DateOnly startDate);
    }
}
