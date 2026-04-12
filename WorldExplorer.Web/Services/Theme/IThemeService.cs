namespace WorldExplorer.Web.Services.Theme
{
    public interface IThemeService
    {
        event Func<Task>? OnThemeChanged;
        string Theme { get; }
        Task InitializeAsync();
        Task Toggle();
    }
}
