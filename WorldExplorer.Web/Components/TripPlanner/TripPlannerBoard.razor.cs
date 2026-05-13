using Microsoft.AspNetCore.Components;

namespace WorldExplorer.Web.Pages
{
    public partial class TripPlannerBoard
    {

        [Parameter, EditorRequired]
        public IReadOnlyList<TripPlannerModel> Trips { get; set; } = [];

        [Parameter, EditorRequired]
        public IReadOnlyList<(string Name, string Flag)> Countries { get; set; } = [];

        [Parameter, EditorRequired]
        public IReadOnlyList<(string Value, string Label)> TripTypes { get; set; } = [];

        [Parameter, EditorRequired]
        public EventCallback<Guid> OnDeleteTrip { get; set; }

        [Parameter, EditorRequired]
        public EventCallback OnPlanTripClick { get; set; }

        private string GetCountryFlag(string countryName)
        {
            var match = Countries.FirstOrDefault(c => c.Name == countryName);
            return match.Flag ?? "";
        }

        private string GetTripTypeLabel(string value)
        {
            var match = TripTypes.FirstOrDefault(t => t.Value == value);
            return match.Label ?? value;
        }

        private string GetTripTypeClass(string value) => $"type-{value}";
    }
}