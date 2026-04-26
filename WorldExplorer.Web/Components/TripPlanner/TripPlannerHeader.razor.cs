using Microsoft.AspNetCore.Components;

namespace WorldExplorer.Web.Components.TripPlanner
{
    public partial class TripPlannerHeader
    {

        [Parameter]
        public EventCallback OnPlanTripClick { get; set; }

    }
}