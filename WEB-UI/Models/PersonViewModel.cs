using System.ComponentModel.DataAnnotations;

namespace WEB_UI.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string FirstName { get; set; } = "";

        [Required, MaxLength(80)]
        public string LastName { get; set; } = "";

        [EmailAddress, MaxLength(160)]
        public string? Email { get; set; }
    }
}