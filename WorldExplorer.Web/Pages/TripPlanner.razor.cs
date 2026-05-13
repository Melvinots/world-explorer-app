using Microsoft.AspNetCore.Components;
using WorldExplorer.Services.TripPlanner;

namespace WorldExplorer.Web.Pages
{
    public partial class TripPlanner
    {
        [Inject] private ITripPlannerService TripPlannerService { get; set; } = default!;

        private bool _showModal = false;

        private readonly IReadOnlyList<(string Name, string Flag)> _countries =
        [
            ("Japan", "🇯🇵"), ("Italy", "🇮🇹"), ("France", "🇫🇷"), ("Spain", "🇪🇸"),
            ("Germany", "🇩🇪"), ("United States", "🇺🇸"), ("Canada", "🇨🇦"),
            ("Australia", "🇦🇺"), ("Brazil", "🇧🇷"), ("Mexico", "🇲🇽"),
            ("Thailand", "🇹🇭"), ("South Korea", "🇰🇷"), ("Philippines", "🇵🇭"),
            ("United Kingdom", "🇬🇧"), ("Greece", "🇬🇷"), ("Turkey", "🇹🇷"),
            ("Indonesia", "🇮🇩"), ("India", "🇮🇳"), ("Portugal", "🇵🇹"),
            ("Netherlands", "🇳🇱"), ("Switzerland", "🇨🇭"), ("New Zealand", "🇳🇿")
        ];

        private readonly IReadOnlyList<(string Value, string Label)> _tripTypes =
        [
            ("leisure",   "🏖️ Leisure"),
            ("adventure", "🏔️ Adventure"),
            ("culture",   "🎨 Culture"),
            ("food",      "🍜 Food Tour"),
            ("business",  "💼 Business")
        ];

        private void OpenModal() => _showModal = true;
        private void CloseModal() => _showModal = false;

        private void HandleTripSubmitted(TripPlannerModel trip)
        {
            TripPlannerService.AddTrip(trip);
            CloseModal();
        }

        private void DeleteTrip(Guid id) => TripPlannerService.DeleteTrip(id);
    }
}