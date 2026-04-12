using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Components.Currency;
using WorldExplorer.Web.Services.Currency;

namespace WorldExplorer.Web.Pages
{
    public partial class Currencies
    {
        #region Injects
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;
        #endregion

        #region Fields
        private List<CurrencyModel> _currencies = new();
        private CurrencyChart _chart = default!;
        #endregion

        #region State
        private bool _isLoadingCurrencies = true;
        #endregion

        #region Lifecycle
        protected override async Task OnInitializedAsync()
        {
            _currencies = await CurrencyService.GetCurrenciesAsync();
            _isLoadingCurrencies = false;
        }
        #endregion

        #region Private methods
        private async Task HandleConverted(CurrencyPair? pair)
        {
            if (pair is null)
            {
                _chart.Clear();
                return;
            }

            await _chart.LoadAsync(pair.From, pair.To);
        }

        private void HandleReset()
        {
            _chart.Clear();
        }
        #endregion
    }
}