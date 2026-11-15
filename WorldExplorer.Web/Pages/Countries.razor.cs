using Microsoft.AspNetCore.Components;
using WorldExplorer.Domain.Enums;
using WorldExplorer.Web.Components.Country;
using WorldExplorer.Web.Integrations.RestCountries;
using WorldExplorer.Web.Integrations.RestCountries.Dtos;
using WorldExplorer.Web.Services.Country;

namespace WorldExplorer.Web.Pages
{
    public partial class Countries : IDisposable
    {
        [Inject] private ICountryService CountryService { get; set; } = default!;

        private readonly CancellationTokenSource _cts = new();

        private bool _isLoading = true;
        private string? _error;
        private string _searchText = string.Empty;
        private Region _selectedRegion = Region.All;
        private CountryModal _modal = default!;

        public List<CountryModel> CountryList { get; set; } = new();

        public int TotalCount => CountryList.Count;
        public int FilteredCount => FilteredCountries.Count;

        private List<CountryModel> FilteredCountries =>
            CountryList.Where(MatchesFilter).ToList();

        protected override async Task OnInitializedAsync() => await LoadAsync();

        private async Task LoadAsync()
        {
            _isLoading = true;
            _error = null;

            try
            {
                CountryList = await CountryService.GetCountryListAsync(_cts.Token);
            }
            catch (CountryApiException ex)
            {
                _error = ex.Message;
            }
            catch (OperationCanceledException)
            {
                // user navigated away, nothing to show
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OnSearchChanged(string? text)
            => _searchText = text ?? string.Empty;

        private async Task OnCardClicked(string countryName)
            => await _modal.OpenAsync(countryName);

        private bool MatchesFilter(CountryModel c)
        {
            var matchesSearch = string.IsNullOrWhiteSpace(_searchText) ||
                c.Names.Common.Contains(_searchText, StringComparison.OrdinalIgnoreCase) ||
                c.Capitals.Any(cap => cap.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase));

            var matchesRegion = _selectedRegion == Region.All ||
                string.Equals(c.Region, _selectedRegion.ToString(), StringComparison.OrdinalIgnoreCase);

            return matchesSearch && matchesRegion;
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}