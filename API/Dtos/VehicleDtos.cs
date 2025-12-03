using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class VehicleCreateUpdateDto
    {
        [Required, RegularExpression(@"^[A-Z0-9]{5,8}$", ErrorMessage = "Plate must be 5-8 alphanumeric uppercase characters")]

        public string Plate { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int? OwnerId { get; set; }
    }

    public class VehicleReadDto
    {
        public int Id { get; set; }
        public string Plate { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int? OwnerId { get; set; }
    }

    public class VehicleWithOwnerDto
    {
        public int Id { get; set; }
        public string Plate { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
    }

}
