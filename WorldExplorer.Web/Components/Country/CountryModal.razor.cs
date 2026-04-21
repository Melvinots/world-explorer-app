using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Country;

namespace WorldExplorer.Web.Components.Country
{
    public partial class CountryModal
    {
        [Inject] private ICountryService CountryService { get; set; } = default!;

        private bool _isVisible = false;
        private bool _isLoading = false;
        private CountryModel? _country;

        public async Task OpenAsync(string countryName)
        {
            SetLoadingState();
            _country = await CountryService.GetCountryByNameAsync(countryName);
            SetLoadedState();
        }

        private void Close() => Reset();

        private void SetLoadingState()
        {
            _isVisible = true;
            _isLoading = true;
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
            _country = null;
        }
    }
}