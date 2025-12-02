using System.ComponentModel.DataAnnotations;

namespace WEB_UI.Models
{
    // Representa exactamente lo que devuelve GET /api/Vehicles/with-owner
    public class VehicleWithOwnerViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Plate { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        public int? OwnerId { get; set; }

        public string? OwnerName { get; set; }
    }
}
