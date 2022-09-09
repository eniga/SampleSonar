using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MicroOrm.Dapper.Repositories.Attributes;

namespace SampleSonar.Data.Entities
{
    public class BaseEntity
    {
        [Identity, Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), ReadOnly(true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed), ReadOnly(true)]
        [UpdatedAt]
        public DateTime? UpdatedAt { get; set; }
    }
}
