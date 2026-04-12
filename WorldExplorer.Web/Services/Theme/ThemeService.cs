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
            // JS handles the visual change instantly
            // Blazor just syncs its state after
            var newTheme = await JS.InvokeAsync<string>("toggleTheme");
            Theme = newTheme;
            OnThemeChanged?.Invoke();
        }
    }
}
