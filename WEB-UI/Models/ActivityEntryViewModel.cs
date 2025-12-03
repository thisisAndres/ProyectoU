namespace WEB_UI.Models
{
    public class ActivityEntryViewModel
    {
        public DateTime Timestamp { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Ej: "Vehículos", "Personas", "Propietarios"
        public string Category { get; set; } = string.Empty;

        // Para estilos Bootstrap: "success", "info", "warning", "danger", "secondary"
        public string Level { get; set; } = "info";
    }
}
