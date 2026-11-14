using System.Text.Json.Serialization;

namespace WorldExplorer.Web.Integrations.RestCountries.Dtos
{
    public class CountriesResponse
    {
        [JsonPropertyName("data")]
        public CountriesData Data { get; set; } = new();
    }

    public class CountriesData
    {
        [JsonPropertyName("objects")]
        public List<CountryDto> Objects { get; set; } = new();

        [JsonPropertyName("meta")]
        public PageMeta Meta { get; set; } = new();
    }

    public class PageMeta
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("more")]
        public bool More { get; set; }
    }

    // ---------- Country ----------

    public class CountryDto
    {
        [JsonPropertyName("uuid")]
        public string Id { get; set; } = "";

        [JsonPropertyName("names")]
        public CountryNames Names { get; set; } = new();

        [JsonPropertyName("codes")]
        public CountryCodes Codes { get; set; } = new();

        [JsonPropertyName("flag")]
        public CountryFlag Flag { get; set; } = new();

        [JsonPropertyName("capitals")]
        public List<Capital> Capitals { get; set; } = new();

        [JsonPropertyName("region")]
        public string Region { get; set; } = "";

        [JsonPropertyName("subregion")]
        public string Subregion { get; set; } = "";

        [JsonPropertyName("continents")]
        public List<string> Continents { get; set; } = new();

        [JsonPropertyName("coordinates")]
        public LatLng Coordinates { get; set; } = new();

        [JsonPropertyName("area")]
        public Area Area { get; set; } = new();

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; set; } = new();

        [JsonPropertyName("currencies")]
        public List<Currency> Currencies { get; set; } = new();

        [JsonPropertyName("descriptions")]
        public Descriptions Descriptions { get; set; } = new();

        [JsonPropertyName("government_type")]
        public string GovernmentType { get; set; } = "";

        [JsonPropertyName("memberships")]
        public Memberships Memberships { get; set; } = new();

        [JsonPropertyName("cars")]
        public Cars Cars { get; set; } = new();

        [JsonPropertyName("links")]
        public CountryLinks Links { get; set; } = new();
    }

    // ---------- Names / codes ----------

    public class CountryNames
    {
        [JsonPropertyName("common")]
        public string Common { get; set; } = "";

        [JsonPropertyName("official")]
        public string Official { get; set; } = "";
    }

    public class NameVariant
    {
        [JsonPropertyName("common")]
        public string Common { get; set; } = "";

        [JsonPropertyName("official")]
        public string Official { get; set; } = "";
    }

    public class CountryCodes
    {
        [JsonPropertyName("alpha_2")]
        public string Alpha2 { get; set; } = "";

        [JsonPropertyName("alpha_3")]
        public string Alpha3 { get; set; } = "";
    }

    // ---------- Flag ----------

    public class CountryFlag
    {
        [JsonPropertyName("emoji")]
        public string Emoji { get; set; } = "";

        [JsonPropertyName("url_svg")]
        public string UrlSvg { get; set; } = "";

        [JsonPropertyName("url_png")]
        public string UrlPng { get; set; } = "";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [JsonPropertyName("colors")]
        public FlagColors Colors { get; set; } = new();
    }

    public class FlagColors
    {
        [JsonPropertyName("dominant")]
        public string Dominant { get; set; } = "";

        [JsonPropertyName("palette")]
        public List<PaletteColor> Palette { get; set; } = new();
    }

    public class PaletteColor
    {
        [JsonPropertyName("hex")]
        public string Hex { get; set; } = "";

        [JsonPropertyName("proportion")]
        public double Proportion { get; set; }
    }

    // ---------- Geography ----------

    public class Capital
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("coordinates")]
        public LatLng Coordinates { get; set; } = new();
    }

    public class LatLng
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class Area
    {
        [JsonPropertyName("kilometers")]
        public double Kilometers { get; set; }

        [JsonPropertyName("miles")]
        public double Miles { get; set; }
    }

    // ---------- People / economy ----------

    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("native_name")]
        public string NativeName { get; set; } = "";

        [JsonPropertyName("iso639_3")]
        public string Iso639_3 { get; set; } = "";
    }

    public class Currency
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }

    public class Descriptions
    {
        [JsonPropertyName("short")]
        public string Short { get; set; } = "";

        [JsonPropertyName("long")]
        public string Long { get; set; } = "";
    }

    public class Memberships
    {
        [JsonPropertyName("un")] public bool Un { get; set; }
        [JsonPropertyName("eu")] public bool Eu { get; set; }
        [JsonPropertyName("nato")] public bool Nato { get; set; }
        [JsonPropertyName("g7")] public bool G7 { get; set; }
        [JsonPropertyName("g20")] public bool G20 { get; set; }
        [JsonPropertyName("brics")] public bool Brics { get; set; }
        [JsonPropertyName("oecd")] public bool Oecd { get; set; }
        [JsonPropertyName("opec")] public bool Opec { get; set; }
        [JsonPropertyName("asean")] public bool Asean { get; set; }
        [JsonPropertyName("commonwealth")] public bool Commonwealth { get; set; }
        [JsonPropertyName("african_union")] public bool AfricanUnion { get; set; }
        [JsonPropertyName("arab_league")] public bool ArabLeague { get; set; }
        [JsonPropertyName("eurozone")] public bool Eurozone { get; set; }
        [JsonPropertyName("schengen")] public bool Schengen { get; set; }

        [JsonIgnore]
        public IEnumerable<string> Active
        {
            get
            {
                if (Un) yield return "UN";
                if (Eu) yield return "EU";
                if (Eurozone) yield return "Eurozone";
                if (Schengen) yield return "Schengen";
                if (Nato) yield return "NATO";
                if (G7) yield return "G7";
                if (G20) yield return "G20";
                if (Brics) yield return "BRICS";
                if (Oecd) yield return "OECD";
                if (Opec) yield return "OPEC";
                if (Asean) yield return "ASEAN";
                if (Commonwealth) yield return "Commonwealth";
                if (AfricanUnion) yield return "African Union";
                if (ArabLeague) yield return "Arab League";
            }
        }
    }

    // ---------- Misc ----------

    public class Cars
    {
        [JsonPropertyName("driving_side")]
        public string DrivingSide { get; set; } = "";
    }

    public class CountryLinks
    {
        [JsonPropertyName("wikipedia")]
        public string Wikipedia { get; set; } = "";

        [JsonPropertyName("google_maps")]
        public string GoogleMaps { get; set; } = "";

        [JsonPropertyName("open_street_maps")]
        public string OpenStreetMaps { get; set; } = "";

        [JsonPropertyName("official")]
        public string Official { get; set; } = "";
    }
}
