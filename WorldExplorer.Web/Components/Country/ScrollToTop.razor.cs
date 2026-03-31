using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WorldExplorer.Web.Components.Country
{
    public partial class ScrollToTop : IDisposable
    {
        // -------------------------
        // Fields
        // -------------------------
        private bool _isVisible = false;
        private DotNetObjectReference<ScrollToTop>? _dotNetRef;

        // -------------------------
        // Injections
        // -------------------------
        [Inject] private IJSRuntime JS { get; set; } = default!;

        // -------------------------
        // Lifecycle
        // -------------------------
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            _dotNetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("registerScrollListener", _dotNetRef);
        }

        public void Dispose()
        {
            _dotNetRef?.Dispose();
        }

        // -------------------------
        // Event Handlers
        // -------------------------
        private async Task ScrollToTopAsync()
            => await JS.InvokeVoidAsync("scrollToTop");

        // -------------------------
        // JS Invokable
        // -------------------------
        [JSInvokable]
        public void UpdateVisibility(bool isVisible)
        {
            _isVisible = isVisible;
            InvokeAsync(StateHasChanged);
        }
    }
}
