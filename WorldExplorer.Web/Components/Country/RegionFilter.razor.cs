using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using WorldExplorer.Domain.Enums;

namespace WorldExplorer.Web.Components.Country
{
    public partial class RegionFilter
    {
        // -------------------------
        // Fields
        // -------------------------
        private bool _isOpen = false;
        private readonly List<Region> _regions = Enum.GetValues<Region>().ToList();

        // -------------------------
        // Parameters
        // -------------------------
        [Parameter] public Region SelectedRegion { get; set; } = Region.All;
        [Parameter] public EventCallback<Region> SelectedRegionChanged { get; set; }

        // -------------------------
        // Event Handlers
        // -------------------------
        private void ToggleDropdown() => _isOpen = !_isOpen;

        private async Task SelectRegion(Region region)
        {
            _isOpen = false;
            SelectedRegion = region;
            await SelectedRegionChanged.InvokeAsync(region);
        }

        private void OnFocusOut(FocusEventArgs e)
        {
            Task.Delay(200).ContinueWith(_ => InvokeAsync(() =>
            {
                _isOpen = false;
                StateHasChanged();
            }));
        }
    }
}