namespace WorldExplorer.Web.Components.Country
{
    public partial class CountryModal
    {
        // -------------------------
        // Fields
        // -------------------------
        private bool _isVisible = false;
        private bool _isLoading = false;
        private CountryModel? _country;

        // -------------------------
        // Public Methods
        // -------------------------
        public async Task OpenAsync(string countryName)
        {
            SetLoadingState();
            _country = await CountryService.GetCountryByNameAsync(countryName);
            SetLoadedState();
        }

        // -------------------------
        // Event Handlers
        // -------------------------
        private void Close() => Reset();

        // -------------------------
        // Helpers
        // -------------------------
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