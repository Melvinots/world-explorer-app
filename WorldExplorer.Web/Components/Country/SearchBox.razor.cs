using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WorldExplorer.Web.Components.Country
{
    public partial class SearchBox
    {
        // -------------------------
        // Parameters
        // -------------------------
        [Parameter] public string Value { get; set; } = string.Empty;
        [Parameter] public string Placeholder { get; set; } = "Search...";
        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        // -------------------------
        // Event Handlers
        // -------------------------
        private async Task OnInputChanged(ChangeEventArgs e)
            => await NotifyChanged(e.Value?.ToString() ?? string.Empty);

        private async Task ClearSearch()
            => await NotifyChanged(string.Empty);

        // -------------------------
        // Helpers
        // -------------------------
        private async Task NotifyChanged(string value)
        {
            Value = value;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}