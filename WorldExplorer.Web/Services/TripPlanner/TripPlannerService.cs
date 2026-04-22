using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorldExplorer.Services.TripPlanner
{
    public class TripPlannerService : ITripPlannerService
    {
        private readonly List<TripPlannerModel> _trips = new();

        public IReadOnlyList<TripPlannerModel> Trips => _trips.AsReadOnly();

        public void AddTrip(TripPlannerModel trip)
        {
            trip.Id = Guid.NewGuid();
            _trips.Insert(0, trip);
        }

        public void DeleteTrip(Guid id)
        {
            var trip = _trips.Find(t => t.Id == id);
            if (trip != null)
                _trips.Remove(trip);
        }
    }
}