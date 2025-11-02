namespace API.Dtos
{
    public class VehicleCreateUpdateDto
    {
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
