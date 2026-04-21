using Microsoft.JSInterop;

namespace WorldExplorer.Web.Services.Theme
{
    public class ThemeService : IThemeService
    {
        private readonly IJSRuntime JS;
        public ThemeService(IJSRuntime js) => JS = js;

        public string Theme { get; private set; } = "dark";
        public bool IsDarkMode => Theme == "dark";

        public event Action? OnThemeChanged;

        public async Task InitializeAsync()
        {
            var saved = await JS.InvokeAsync<string?>("localStorage.getItem", "theme");
            Theme = saved ?? "dark";
        }

        public async Task Toggle()
        {
            var newTheme = await JS.InvokeAsync<string>("toggleTheme");
            Theme = newTheme;
            OnThemeChanged?.Invoke();
        }
    }
}
