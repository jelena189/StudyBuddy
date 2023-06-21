using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Repositories.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        public bool IsArchived { get; set; }

        public DateTime ArchivedOn { get; set; }

        public Guid? AuthorId { get; set; }

        public User? Author { get; set; }

        public Guid? ClassId { get; set; }

        public Class Class { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
