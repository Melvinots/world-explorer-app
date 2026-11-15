using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Components.Currency;
using WorldExplorer.Web.Services.Currency;

namespace WorldExplorer.Web.Pages
{
    public partial class Currencies : IDisposable
    {
        [Inject] private ICurrencyService CurrencyService { get; set; } = default!;
        [Inject] private ILogger<Currencies> Logger { get; set; } = default!;

        private readonly CancellationTokenSource _pageCts = new();
        private CancellationTokenSource? _chartCts;

        private List<CurrencyModel> _currencies = new();
        private CurrencyChart? _chart;

        private bool _isLoadingCurrencies = true;
        private string? _loadError;
        private string? _chartError;

        protected override Task OnInitializedAsync() => LoadCurrenciesAsync();

        private Task RetryLoadAsync() => LoadCurrenciesAsync();

        private async Task LoadCurrenciesAsync()
        {
            _isLoadingCurrencies = true;
            _loadError = null;

            try
            {
                _currencies = await CurrencyService.GetCurrenciesAsync(_pageCts.Token) ?? new();
            }
            catch (OperationCanceledException) when (_pageCts.IsCancellationRequested)
            {
                // The user left the page. Nothing to show.
            }
            catch (OperationCanceledException ex)
            {
                Logger.LogWarning(ex, "Loading currencies timed out.");
                _loadError = "Loading currencies took too long. Please try again.";
            }
            catch (HttpRequestException ex)
            {
                Logger.LogWarning(ex, "Currency service request failed.");
                _loadError = "Couldn't reach the currency service. Check your connection and try again.";
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unexpected error while loading currencies.");
                _loadError = "Something went wrong while loading currencies.";
            }
            finally
            {
                _isLoadingCurrencies = false;
            }
        }

        private async Task HandleConverted(CurrencyPair? pair)
        {
            _chartError = null;

            if (pair is null)
            {
                CancelChartRequest();
                _chart?.Clear();
                return;
            }

            if (_chart is null ||
                string.IsNullOrWhiteSpace(pair.From) ||
                string.IsNullOrWhiteSpace(pair.To))
            {
                return;
            }

            CancelChartRequest();
            var cts = CancellationTokenSource.CreateLinkedTokenSource(_pageCts.Token);
            _chartCts = cts;

            try
            {
                await _chart.LoadAsync(pair.From, pair.To, cts.Token);
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                // Superseded by a newer request, reset, or the user left the page.
            }
            catch (OperationCanceledException ex)
            {
                Logger.LogWarning(ex, "Chart request for {From}/{To} timed out.", pair.From, pair.To);
                if (IsCurrent(cts)) _chartError = "The chart took too long to load. Please try again.";
            }
            catch (HttpRequestException ex)
            {
                Logger.LogWarning(ex, "Chart request for {From}/{To} failed.", pair.From, pair.To);
                if (IsCurrent(cts)) _chartError = "Couldn't load the exchange rate history right now.";
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unexpected error loading chart for {From}/{To}.", pair.From, pair.To);
                if (IsCurrent(cts)) _chartError = "Something went wrong while loading the chart.";
            }
            finally
            {
                if (IsCurrent(cts)) _chartCts = null;
                cts.Dispose();
            }
        }

        private void HandleReset()
        {
            CancelChartRequest();
            _chartError = null;
            _chart?.Clear();
        }

        private void DismissChartError() => _chartError = null;

        private bool IsCurrent(CancellationTokenSource cts) => ReferenceEquals(_chartCts, cts);

        private void CancelChartRequest()
        {
            var cts = _chartCts;
            _chartCts = null;
            cts?.Cancel();
        }

        public void Dispose()
        {
            _pageCts.Cancel();
            _pageCts.Dispose();
        }
    }
}