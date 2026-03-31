using System.Text.Json.Serialization;

namespace WorldExplorer.Domain.Models
{
    public class CountryModel
    {
        [JsonPropertyName("tld")]
        public string[]? Tld { get; set; }

        [JsonPropertyName("cca2")]
        public string? Cca2 { get; set; }

        [JsonPropertyName("ccn3")]
        public string? Ccn3 { get; set; }

        [JsonPropertyName("cca3")]
        public string? Cca3 { get; set; }

        [JsonPropertyName("cioc")]
        public string? Cioc { get; set; }

        [JsonPropertyName("independent")]
        public bool Independent { get; set; } = false;

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("unMember")]
        public bool UnMember { get; set; } = false;

        [JsonPropertyName("capital")]
        public string[]? Capital { get; set; }

        [JsonPropertyName("altSpellings")]
        public string[]? AltSpellings { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("subregion")]
        public string? Subregion { get; set; }

        [JsonPropertyName("landlocked")]
        public bool Landlocked { get; set; } = false;

        [JsonPropertyName("area")]
        public decimal? Area { get; set; }

        [JsonPropertyName("maps")]
        public Maps? Maps { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("fifa")]
        public string? Fifa { get; set; }

        [JsonPropertyName("timezones")]
        public string[]? Timezones { get; set; }

        [JsonPropertyName("continents")]
        public string[]? Continents { get; set; }

        [JsonPropertyName("flag")]
        public string? Flag { get; set; }

        [JsonPropertyName("name")]
        public Name? Name { get; set; }

        [JsonPropertyName("currencies")]
        public Dictionary<string, Currency>? Currencies { get; set; }

        [JsonPropertyName("languages")]
        public Dictionary<string, string>? Languages { get; set; }

        [JsonPropertyName("flags")]
        public Flags? Flags { get; set; }

        [JsonPropertyName("coatOfArms")]
        public CoatOfArms? CoatOfArms { get; set; }
    }

    public partial class CoatOfArms
    {
        [JsonPropertyName("png")]
        public Uri? Png { get; set; }

        [JsonPropertyName("svg")]
        public Uri? Svg { get; set; }
    }

    public partial class Currency
    {
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public partial class Flags
    {
        [JsonPropertyName("png")]
        public Uri? Png { get; set; }

        [JsonPropertyName("svg")]
        public Uri? Svg { get; set; }

        [JsonPropertyName("alt")]
        public string? Alt { get; set; }
    }

    public partial class Maps
    {
        [JsonPropertyName("googleMaps")]
        public Uri? GoogleMaps { get; set; }

        [JsonPropertyName("openStreetMaps")]
        public Uri? OpenStreetMaps { get; set; }
    }

    public partial class Name
    {
        [JsonPropertyName("common")]
        public string? Common { get; set; }

        [JsonPropertyName("official")]
        public string? Official { get; set; }

        [JsonPropertyName("nativeName")]
        public Dictionary<string, NativeName>? NativeName { get; set; }
    }

    public partial class NativeName
    {
        [JsonPropertyName("official")]
        public string? Official { get; set; }

        [JsonPropertyName("common")]
        public string? Common { get; set; }
    }
}