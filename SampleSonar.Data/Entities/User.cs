using System.ComponentModel.DataAnnotations.Schema;

namespace SampleSonar.Data.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
