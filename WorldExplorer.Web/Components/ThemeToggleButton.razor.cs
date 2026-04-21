using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Theme;

namespace WorldExplorer.Web.Components
{
    public partial class ThemeToggleButton
    {
        [Inject] private IThemeService ThemeService { get; set; } = default!;

        private async Task ToggleTheme()
        {
            await ThemeService.Toggle();
        }
    }
}