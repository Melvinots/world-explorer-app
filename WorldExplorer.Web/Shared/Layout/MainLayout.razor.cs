using ApexCharts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WorldExplorer.Web.Services.Theme;

namespace WorldExplorer.Web.Shared.Layout
{
    public partial class MainLayout : IDisposable
    {
        [Inject] private IThemeService ThemeService { get; set; } = default!;

        public bool IsDarkMode => ThemeService.Theme == "dark";

        protected override Task OnInitializedAsync()
        {
            ThemeService.OnThemeChanged += OnThemeChangedHandler;
            return Task.CompletedTask;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            await ThemeService.InitializeAsync();
            await InvokeAsync(StateHasChanged);
        }

        private void OnThemeChangedHandler()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            ThemeService.OnThemeChanged -= OnThemeChangedHandler;
        }

        #region Theme
        private MudTheme _myTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Background = "#ffffff",
                Surface = "#ffffff",
                TextPrimary = "#2a2d3e",
                TextSecondary = "#4a4f6a",
                TextDisabled = "#9da3b4",
                Primary = "#1a7fd4",
                Divider = "#e0e4f0",
                DrawerBackground = "#f8f9ff",
                AppbarBackground = "rgba(255, 255, 255, 0.75)",
                ActionDefault = "#4a4f6a",
            },
            PaletteDark = new PaletteDark()
            {
                Background = "#1e2130",
                Surface = "#252837",
                TextPrimary = "#c8cbe0",
                TextSecondary = "#8b90a8",
                TextDisabled = "#5a5f78",
                Primary = "#3b9eff",
                Divider = "#313449",
                DrawerBackground = "#181b27",
                AppbarBackground = "rgba(30, 33, 48, 0.75)",
                ActionDefault = "#8b90a8",
            }
        };
        #endregion
    }
}