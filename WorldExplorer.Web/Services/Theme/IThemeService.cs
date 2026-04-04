namespace WorldExplorer.Web.Services.Theme
{
    public interface IThemeService
    {
        string Theme { get; }
        Task InitializeAsync();
        Task Toggle();
    }
}
