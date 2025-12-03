namespace WEB_UI.Models
{
    public class DashboardViewModel
    {
        public int TotalVehicles { get; set; }
        public int TotalPersons { get; set; }
        public int VehiclesWithOwner { get; set; }
        public int VehiclesWithoutOwner { get; set; }

        public List<ActivityEntryViewModel> RecentActivity { get; set; } = new();
    }
}
