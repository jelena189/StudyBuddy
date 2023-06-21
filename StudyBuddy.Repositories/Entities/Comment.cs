using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Repositories.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        public Guid PostId { get; set; }

        public Post Post { get; set; }

        public Guid? AuthorId { get; set; }

        public User? Author { get; set; }
    }
}
