using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Currency;

namespace WorldExplorer.Web.Components.Currency
{
    public partial class CurrencyConverter
    {
        #region Injects
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;
        #endregion

        #region Parameters
        [Parameter] public List<CurrencyModel> Currencies { get; set; } = new();
        [Parameter] public EventCallback<CurrencyPair> OnConverted { get; set; }
        [Parameter] public EventCallback OnReset { get; set; }
        #endregion

        #region Constants
        private const decimal BaseAmount = 1m;
        private const int DecimalPlaces = 4;
        #endregion

        #region Fields
        private string _fromCurrency = "USD";
        private string _toCurrency = "PHP";
        private decimal _amount = 1;
        private decimal _baseRate = 0;
        private ConversionResult? _conversionResult;
        #endregion

        #region State
        private bool _isConverting = true;
        #endregion

        #region Lifecycle
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ConvertAsync();
        }
        #endregion

        #region Actions
        private async Task ConvertAsync()
        {
            var currencyPair = new CurrencyPair { From = _fromCurrency, To = _toCurrency };
            
            _isConverting = true;
            if (_fromCurrency == _toCurrency)
            {
                _conversionResult = new ConversionResult
                {
                    Amount = _amount,
                    Base = _fromCurrency,
                    Date = DateTime.Today.ToString("yyyy-MM-dd"),
                    Rates = new Dictionary<string, decimal> { { _toCurrency, _amount } }
                };
                _baseRate = 1;
                _isConverting = false;
                await OnConverted.InvokeAsync(null);
                return;
            }

            var result = await CurrencyService.ConvertAsync(_fromCurrency, _toCurrency, BaseAmount);

            if (result is not null)
            {
                _baseRate = result.Rates[_toCurrency];
                result.Rates[_toCurrency] = Math.Round(_baseRate * _amount, DecimalPlaces);
                _conversionResult = result;
            }

            _isConverting = false;
            await OnConverted.InvokeAsync(currencyPair);
        }

        private void SwapCurrencies()
        {
            if (_fromCurrency == _toCurrency)
                return;

            (_fromCurrency, _toCurrency) = (_toCurrency, _fromCurrency);
            ResetConversion();
        }
        #endregion

        #region Helpers
        private void RecalculateAsync()
        {
            if (_baseRate == 0 || _conversionResult is null)
                return;

            _conversionResult.Rates[_toCurrency] = Math.Round(_baseRate * _amount, DecimalPlaces);
        }

        private void ResetConversion()
        {
            _conversionResult = new ConversionResult
            {
                Amount = _amount,
                Base = _fromCurrency,
                Date = "--/--",
                Rates = new Dictionary<string, decimal> { { _toCurrency, 0 } }
            };
            _baseRate = 0;
            OnReset.InvokeAsync();
        }
        #endregion
    }
}