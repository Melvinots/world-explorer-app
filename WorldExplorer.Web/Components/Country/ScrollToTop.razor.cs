using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WorldExplorer.Web.Components.Country
{
    public partial class ScrollToTop
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;

        private async Task ScrollToTopAsync()
            => await JS.InvokeVoidAsync("scrollToTop");
    }
}
