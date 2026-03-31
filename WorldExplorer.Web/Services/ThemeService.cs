using Microsoft.JSInterop;

namespace WorldExplorer.Web.Services
{
    public class ThemeService : IThemeService
    {
        // -------------------------
        // Fields
        // -------------------------
        private readonly IJSRuntime _js;

        // -------------------------
        // Constructor
        // -------------------------
        public ThemeService(IJSRuntime js) => _js = js;

        // -------------------------
        // Properties
        // -------------------------
        public string Theme { get; private set; } = "light";

        // -------------------------
        // Public Methods
        // -------------------------
        public async Task InitializeAsync()
        {
            var saved = await _js.InvokeAsync<string?>("localStorage.getItem", "theme");
            Theme = saved ?? "light";
        }

        public async Task Toggle()
        {
            Theme = Theme == "light" ? "dark" : "light";
            await PersistThemeAsync();
            await ApplyThemeAsync();
        }

        // -------------------------
        // Helpers
        // -------------------------
        private async Task PersistThemeAsync()
            => await _js.InvokeVoidAsync("localStorage.setItem", "theme", Theme);

        private async Task ApplyThemeAsync()
            => await _js.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", Theme);
    }
}
