using System.ComponentModel.DataAnnotations;

namespace WEB_UI.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9]{5,8}$",
            ErrorMessage = "La placa debe tener de 5 a 8 caracteres alfanuméricos en mayúscula")]
        public string Plate { get; set; } = "";

        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";

        public int? OwnerId { get; set; }
    }
}
