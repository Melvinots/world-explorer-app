using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Components.Currency;
using WorldExplorer.Web.Services.Currency;

namespace WorldExplorer.Web.Pages
{
    public partial class Currencies
    {
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;

        private List<CurrencyModel> _currencies = new();
        private CurrencyChart _chart = default!;
        private bool _isLoadingCurrencies = true;

        protected override async Task OnInitializedAsync()
        {
            _currencies = await CurrencyService.GetCurrenciesAsync();
            _isLoadingCurrencies = false;
        }

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
    }
}