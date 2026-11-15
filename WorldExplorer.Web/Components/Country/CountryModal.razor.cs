using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Integrations.RestCountries;
using WorldExplorer.Web.Integrations.RestCountries.Dtos;
using WorldExplorer.Web.Services.Country;

namespace WorldExplorer.Web.Components.Country
{
    public partial class CountryModal : IDisposable
    {
        [Inject] private ICountryService CountryService { get; set; } = default!;

        private CancellationTokenSource? _cts;

        private bool _isVisible;
        private bool _isLoading;
        private bool _notFound;
        private string? _error;
        private CountryModel? _country;

        public async Task OpenAsync(string countryName)
        {
            CancelPending();
            var cts = _cts = new CancellationTokenSource();

            SetLoadingState();

            try
            {
                _country = await CountryService.GetCountryByNameAsync(countryName, cts.Token);
                _notFound = _country is null;
            }
            catch (CountryApiException ex)
            {
                _error = ex.Message;
            }
            catch (OperationCanceledException)
            {
                return;
            }

            if (cts.IsCancellationRequested) return;

            SetLoadedState();
        }

        private void Close()
        {
            CancelPending();
            Reset();
        }

        private void SetLoadingState()
        {
            _isVisible = true;
            _isLoading = true;
            _notFound = false;
            _error = null;
            _country = null;
            StateHasChanged();
        }

        private void SetLoadedState()
        {
            _isLoading = false;
            StateHasChanged();
        }

        private void Reset()
        {
            _isVisible = false;
            _isLoading = false;
            _notFound = false;
            _error = null;
            _country = null;
        }

        private void CancelPending()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void Dispose() => CancelPending();
    }
}