using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Theme;

namespace WorldExplorer.Web.Components
{
    public partial class ThemeToggleButton
    {
        // -------------------------
        // Injections
        // -------------------------
        [Inject] private IThemeService ThemeService { get; set; } = default!;

        // -------------------------
        // Event Handlers
        // -------------------------
        private async Task ToggleTheme()
        {
            await ThemeService.Toggle();
        }
    }
}