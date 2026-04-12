using Microsoft.AspNetCore.Components;
using WorldExplorer.Web.Services.Theme;

namespace WorldExplorer.Web.Components
{
    public abstract class ThemeAwareComponent : ComponentBase, IDisposable
    {
        [Inject] protected IThemeService ThemeService { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {
            ThemeService.OnThemeChanged += OnThemeChangedHandler;
            return Task.CompletedTask;
        }

        private void OnThemeChangedHandler()
        {
            InvokeAsync(StateHasChanged);
        }

        public virtual void Dispose()
        {
            ThemeService.OnThemeChanged -= StateHasChanged;
        }
    }
}
