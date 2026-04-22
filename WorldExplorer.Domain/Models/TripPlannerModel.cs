using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldExplorer.Domain.Models
{
    public class TripPlannerModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Your name is required.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        public string TravelerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a destination.")]
        public string DestinationCountry { get; set; } = string.Empty;

        public string CountryFlag { get; set; } = string.Empty;

        [Required(ErrorMessage = "Departure date is required.")]
        public DateTime? DepartureDate { get; set; }

        [Required(ErrorMessage = "Return date is required.")]
        public DateTime? ReturnDate { get; set; }

        [Required(ErrorMessage = "Number of travelers is required.")]
        [Range(1, 20, ErrorMessage = "Travelers must be between 1 and 20.")]
        public int? Travelers { get; set; }

        [Required(ErrorMessage = "Budget is required.")]
        [Range(1, 999999, ErrorMessage = "Budget must be greater than 0.")]
        public decimal? BudgetUSD { get; set; }

        [Required(ErrorMessage = "Please select a trip type.")]
        public string TripType { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        // Computed properties
        public int DurationDays =>
            (DepartureDate.HasValue && ReturnDate.HasValue)
                ? (int)(ReturnDate.Value - DepartureDate.Value).TotalDays
                : 0;

        public decimal BudgetPerPerson =>
            (BudgetUSD.HasValue && Travelers.HasValue && Travelers > 0)
                ? Math.Round(BudgetUSD.Value / Travelers.Value, 2)
                : 0;
    }
}
