using System.ComponentModel.DataAnnotations;

namespace SampleSonar.Data.Models.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
