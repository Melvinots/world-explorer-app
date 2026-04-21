using Microsoft.AspNetCore.Components;

namespace WorldExplorer.Web.Components.Country
{
    public partial class SearchBox
    {
        [Parameter] public string Value { get; set; } = string.Empty;
        [Parameter] public string Placeholder { get; set; } = "Search...";
        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        private async Task OnInputChanged(ChangeEventArgs e)
            => await NotifyChanged(e.Value?.ToString() ?? string.Empty);

        private async Task ClearSearch()
            => await NotifyChanged(string.Empty);

        private async Task NotifyChanged(string value)
        {
            Value = value;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}