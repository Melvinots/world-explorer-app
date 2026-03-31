namespace WorldExplorer.Web.Services
{
    public interface IThemeService
    {
        string Theme { get; }
        Task InitializeAsync();
        Task Toggle();
    }
}
