using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace WorldExplorer.Web.Integrations.RestCountries.Dtos
{
    public class CountryModel
    {
        public string Id { get; set; } = "";
        public CountryNames Names { get; set; } = new();
        public CountryCodes Codes { get; set; } = new();
        public CountryFlag Flag { get; set; } = new();
        public List<Capital> Capitals { get; set; } = new();
        public string Region { get; set; } = "";
        public string Subregion { get; set; } = "";
        public List<string> Continents { get; set; } = new();
        public LatLng Coordinates { get; set; } = new();
        public Area Area { get; set; } = new();
        public long Population { get; set; }
        public List<Language> Languages { get; set; } = new();
        public List<Currency> Currencies { get; set; } = new();
        public Descriptions Descriptions { get; set; } = new();
        public string GovernmentType { get; set; } = "";
        public Memberships Memberships { get; set; } = new();
        public Cars Cars { get; set; } = new();
        public CountryLinks Links { get; set; } = new();
    }
}