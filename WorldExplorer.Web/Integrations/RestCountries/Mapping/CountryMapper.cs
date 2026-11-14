using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldExplorer.Domain.Models;
using WorldExplorer.Web.Integrations.RestCountries.Dtos;

namespace WorldExplorer.Web.Integrations.RestCountries.Mapping
{
    public static class CountryMapper
    {
        public static CountryModel ToModel(CountryDto dto) => new()
        {
            Id = dto.Id,
            Names = dto.Names,
            Codes = dto.Codes,
            Flag = dto.Flag,
            Capitals = dto.Capitals ?? new List<Capital>(),
            Region = NullIfEmpty(dto.Region) ?? "",
            Subregion = NullIfEmpty(dto.Subregion) ?? "",
            Continents = dto.Continents ?? new List<string>(),
            Coordinates = dto.Coordinates ?? new LatLng(),
            Area = dto.Area ?? new Area(),
            Population = dto.Population,
            Languages = dto.Languages ?? new List<Language>(),
            Currencies = dto.Currencies ?? new List<Currency>(),
            Descriptions = dto.Descriptions ?? new Descriptions(),
            GovernmentType = NullIfEmpty(dto.GovernmentType) ?? "",
            Memberships = dto.Memberships ?? new Memberships(),
            Cars = dto.Cars ?? new Cars(),
            Links = dto.Links ?? new CountryLinks()
        };

        private static string? NullIfEmpty(string? value) =>
            string.IsNullOrWhiteSpace(value) ? null : value;
    }
}
