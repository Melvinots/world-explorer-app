using WorldExplorer.Domain.Enums;

namespace WorldExplorer.Domain.Helpers
{
    public static class FormatHelper
    {
        public static string FormatPopulation(long? pop) => pop switch
        {
            >= 1_000_000_000 => $"{pop / 1_000_000_000.0:F2}B",
            >= 1_000_000 => $"{pop / 1_000_000.0:F2}M",
            >= 1_000 => $"{pop / 1_000.0:F2}K",
            _ => pop?.ToString() ?? "N/A"
        };

        public static string ToDisplayName(this Region region) => region switch
        {
            Region.All => "All Regions",
            _ => region.ToString()
        };

        public static string ToKeyString(this CacheKey key) => key switch
        {
            CacheKey.Currencies => "currencies",
            CacheKey.Countries => "countries",
            _ => key.ToString().ToLower()
        };
    }
}