namespace WorldExplorer.Services.TripPlanner
{
    public interface ITripPlannerService
    {
        IReadOnlyList<TripPlannerModel> Trips { get; }
        void AddTrip(TripPlannerModel trip);
        void DeleteTrip(Guid id);
    }
}