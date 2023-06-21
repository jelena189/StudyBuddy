using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Repositories.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
