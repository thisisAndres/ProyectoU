using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class PersonCreateUpdateDto
    {
        [Required, MaxLength(80)]
        public string FirstName { get; set; } = "";
        [Required, MaxLength(80)]
        public string LastName { get; set; } = "";
        [EmailAddress, MaxLength(160)]
        public string? Email { get; set; }
    }

    public class PersonReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
    }

}
