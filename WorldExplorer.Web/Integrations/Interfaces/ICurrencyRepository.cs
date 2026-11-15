using WorldExplorer.Web.Integrations.Frankfurter.Dtos;

namespace WorldExplorer.Web.Integrations.Interfaces
{
    public interface ICurrencyRepository
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
