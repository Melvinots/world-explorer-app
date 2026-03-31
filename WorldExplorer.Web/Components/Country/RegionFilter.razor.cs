using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WorldExplorer.Domain.Enums;

namespace WorldExplorer.Web.Components.Country
{
    public partial class RegionFilter : IDisposable
    {
        // -------------------------
        // Fields
        // -------------------------
        private ElementReference _filterRef;
        private bool _isOpen = false;
        private readonly List<Region> _regions = Enum.GetValues<Region>().ToList();
        private DotNetObjectReference<RegionFilter>? _dotNetRef;

        // -------------------------
        // Injections
        // -------------------------
        [Inject] private IJSRuntime JS { get; set; } = default!;

        // -------------------------
        // Parameters
        // -------------------------
        [Parameter] public Region SelectedRegion { get; set; } = Region.All;
        [Parameter] public EventCallback<Region> SelectedRegionChanged { get; set; }

        // -------------------------
        // Lifecycle
        // -------------------------
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            _dotNetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("registerClickOutside", _filterRef, _dotNetRef);
        }

        public void Dispose()
        {
            _dotNetRef?.Dispose();
        }

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

        // -------------------------
        // JS Invokable
        // -------------------------
        [JSInvokable]
        public void CloseDropdown()
        {
            _isOpen = false;
            StateHasChanged();
        }
    }
}