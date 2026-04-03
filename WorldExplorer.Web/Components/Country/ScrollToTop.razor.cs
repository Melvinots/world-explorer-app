using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WorldExplorer.Web.Components.Country
{
    public partial class ScrollToTop
    {
        // -------------------------
        // Injections
        // -------------------------
        [Inject] private IJSRuntime JS { get; set; } = default!;

        // -------------------------
        // Event Handlers
        // -------------------------
        private async Task ScrollToTopAsync()
            => await JS.InvokeVoidAsync("scrollToTop");
    }
}
