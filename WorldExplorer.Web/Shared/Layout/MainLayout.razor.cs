using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services;

namespace WorldExplorer.Web.Shared.Layout
{
    public partial class MainLayout
    {
        [Inject] private IThemeService ThemeService { get; set; } = default!;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;
            await ThemeService.InitializeAsync();
        }
    }
}