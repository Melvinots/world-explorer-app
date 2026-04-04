using Microsoft.AspNetCore.Components;
using WorldExplorer.Domain.Enums;
using WorldExplorer.Web.Components.Country;
using WorldExplorer.Web.Services.Country;

namespace WorldExplorer.Web.Pages
{
    public partial class Countries
    {
        [Inject] private ICountryService CountryService { get; set; } = default!;

        // -------------------------
        // Fields
        // -------------------------
        private bool _isLoading = true;
        private string _searchText = string.Empty;
        private Region _selectedRegion = Region.All;
        private CountryModal _modal = default!;

        // -------------------------
        // Properties
        // -------------------------
        public List<CountryModel>? CountryList { get; set; }

        public int TotalCount => CountryList?.Count ?? 0;
        public int FilteredCount => FilteredCountries.Count();
        private List<CountryModel> FilteredCountries =>
            CountryList?.Where(MatchesFilter).ToList()
            ?? new List<CountryModel>();

        // -------------------------
        // Lifecycle
        // -------------------------
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            _isLoading = true;
            CountryList = await CountryService.GetCountryListAsync();
            _isLoading = false;

            StateHasChanged();
        }

        // -------------------------
        // Event Handlers
        // -------------------------
        private void OnSearchChanged(string text)
            => _searchText = text ?? string.Empty;

        private async Task OnCardClicked(string countryName)
            => await _modal.OpenAsync(countryName);

        // -------------------------
        // Helpers
        // -------------------------
        private bool MatchesFilter(CountryModel c)
        {
            var matchesSearch = string.IsNullOrWhiteSpace(_searchText) ||
                c.Name?.Common?.Contains(_searchText, StringComparison.OrdinalIgnoreCase) == true ||
                c.Capital?.Any(cap => cap.Contains(_searchText, StringComparison.OrdinalIgnoreCase)) == true;

            var matchesRegion = _selectedRegion == Region.All ||
                c.Region == _selectedRegion.ToString();

            return matchesSearch && matchesRegion;
        }
    }
}