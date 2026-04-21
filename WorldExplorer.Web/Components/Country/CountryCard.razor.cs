using Microsoft.AspNetCore.Components;

namespace WorldExplorer.Web.Components.Country
{

    public partial class CountryCard
    {
        [Parameter] public string? CountryCode { get; set; }
        [Parameter] public string? CountryName { get; set; }
        [Parameter] public string? Capital { get; set; }
        [Parameter] public string? Region { get; set; }
        [Parameter] public string? Population { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
    }
}