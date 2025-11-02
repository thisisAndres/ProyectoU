namespace API.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Plate { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int? OwnerId { get; set; }
        public Person? Owner { get; set; }
    }
}
