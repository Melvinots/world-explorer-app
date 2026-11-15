namespace WorldExplorer.Web.Services.Currency
{
    public interface ICurrencyService
    {
        Task<List<CurrencyModel>> GetCurrenciesAsync(
            CancellationToken cancellationToken = default);

        Task<ExchangeRateResponse?> ConvertAsync(
            string fromCurrency,
            string toCurrency,
            decimal amount = 1,
            CancellationToken cancellationToken = default);

        Task<HistoricalExchangeRateResponse?> GetHistoricalRatesAsync(
            string fromCurrency,
            string toCurrency,
            DateOnly startDate,
            CancellationToken cancellationToken = default);
    }
}
