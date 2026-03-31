using WorldExplorer.Web;
using WorldExplorer.Web.Repositories;
using WorldExplorer.Web.Services;

var builder = WebApplication.CreateBuilder(args);

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });

builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddHttpClient<ICountryRepository, CountryApiRepository>(client =>
{
    client.BaseAddress = new Uri("https://restcountries.com/v3.1/");
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
