using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Repositories.Entities
{
    public class Class : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string ClassName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public int? Year { get; set; }

        public int? Semester { get; set; }

        public Guid SchoolId { get; set; }

        public School School { get; set; }

        public ICollection<Post> Posts { get; set; } 
    }
}
