using ApexCharts;
using MudBlazor.Services;
using WorldExplorer.Services.TripPlanner;
using WorldExplorer.Web;
using WorldExplorer.Web.Repositories.Country;
using WorldExplorer.Web.Repositories.Currency;
using WorldExplorer.Web.Services.Country;
using WorldExplorer.Web.Services.Currency;
using WorldExplorer.Web.Services.Theme;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });

builder.Services.AddApexCharts();
builder.Services.AddMudServices();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ITripPlannerService, TripPlannerService>();
builder.Services.AddHttpClient<ICountryRepository, CountryApiRepository>(client =>
{
    client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
});
builder.Services.AddHttpClient<ICurrencyRepository, CurrencyApiRepository>(client =>
{
    client.BaseAddress = new Uri("https://api.frankfurter.dev/v1/");
});


// Switch to DB later — only change this one line
//builder.Services.AddScoped<ICountryRepository, CountryDbRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
