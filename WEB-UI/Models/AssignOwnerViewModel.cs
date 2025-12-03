using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WEB_UI.Models
{
    public class AssignOwnerViewModel
    {
        // Datos del vehículo
        public int VehicleId { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;

        // Propietario seleccionado
        public int? OwnerId { get; set; }

        // Lista de posibles propietarios (Personas)
        public List<SelectListItem> AvailableOwners { get; set; } = new();
    }
}
