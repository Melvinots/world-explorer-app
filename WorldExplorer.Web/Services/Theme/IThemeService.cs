namespace WorldExplorer.Web.Services.Theme
{
    public interface IThemeService
    {
        event Action? OnThemeChanged;
        string Theme { get; }
        Task InitializeAsync();
        Task Toggle();
    }
}
