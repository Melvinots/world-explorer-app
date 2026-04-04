namespace WorldExplorer.Web.Services.Currency
{
    public interface ICurrencyService
    {
        Task<List<CurrencyModel>> GetCurrenciesAsync();
        Task<ConversionResult?> ConvertAsync(string fromCurrency, string toCurrency, decimal amount = 1);
        Task<HistoricalRatesResult?> GetHistoricalRatesAsync(string fromCurrency, string toCurrency, DateOnly startDate);
    }
}
