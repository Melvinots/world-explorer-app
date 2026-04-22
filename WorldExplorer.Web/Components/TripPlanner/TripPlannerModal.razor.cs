using Microsoft.AspNetCore.Components;

namespace WorldExplorer.Web.Components.TripPlanner
{
    public partial class TripPlannerModal
    {
        [Parameter, EditorRequired]
        public bool IsVisible { get; set; }

        [Parameter, EditorRequired]
        public IReadOnlyList<(string Name, string Flag)> Countries { get; set; } = [];

        [Parameter, EditorRequired]
        public IReadOnlyList<(string Value, string Label)> TripTypes { get; set; } = [];

        [Parameter, EditorRequired]
        public EventCallback<TripPlannerModel> OnTripSubmitted { get; set; }

        [Parameter, EditorRequired]
        public EventCallback OnCancelled { get; set; }

        private TripPlannerModel _newTrip = new();
        private string? _dateError;

        protected override void OnParametersSet()
        {
            if (IsVisible)
            {
                _newTrip = new TripPlannerModel();
                _dateError = null;
            }
        }

        private void OnCountryChanged()
        {
            var match = Countries.FirstOrDefault(c => c.Name == _newTrip.DestinationCountry);
            _newTrip.CountryFlag = match.Flag ?? string.Empty;
        }

        private async Task HandleValidSubmit()
        {
            _dateError = null;

            if (_newTrip.DepartureDate.HasValue && _newTrip.ReturnDate.HasValue)
            {
                if (_newTrip.ReturnDate <= _newTrip.DepartureDate)
                {
                    _dateError = "Return date must be after departure date.";
                    return;
                }
            }

            await OnTripSubmitted.InvokeAsync(_newTrip);
        }

        private async Task HandleCancel() => await OnCancelled.InvokeAsync();

        private async Task HandleOverlayClick() => await OnCancelled.InvokeAsync();
    }
}