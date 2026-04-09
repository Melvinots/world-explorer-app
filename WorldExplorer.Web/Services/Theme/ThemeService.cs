using Microsoft.JSInterop;

namespace WorldExplorer.Web.Services.Theme
{
    public class ThemeService : IThemeService
    {
        // -------------------------
        // Fields
        // -------------------------
        private readonly IJSRuntime JS;

        // -------------------------
        // Constructor
        // -------------------------
        public ThemeService(IJSRuntime js) => JS = js;

        // -------------------------
        // Properties
        // -------------------------
        public string Theme { get; private set; } = "dark";
        public bool IsDarkMode => Theme == "dark";

        public event Action? OnThemeChanged;

        // -------------------------
        // Public Methods
        // -------------------------
        public async Task InitializeAsync()
        {
            var saved = await JS.InvokeAsync<string?>("localStorage.getItem", "theme");
            Theme = saved ?? "dark";
        }

        public async Task Toggle()
        {
            Theme = Theme == "dark" ? "light" : "dark";
            await PersistThemeAsync();
            await ApplyThemeAsync();
            OnThemeChanged?.Invoke();
        }

        // -------------------------
        // Helpers
        // -------------------------
        private async Task PersistThemeAsync()
            => await JS.InvokeVoidAsync("localStorage.setItem", "theme", Theme);

        private async Task ApplyThemeAsync()
            => await JS.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", Theme);
    }
}
