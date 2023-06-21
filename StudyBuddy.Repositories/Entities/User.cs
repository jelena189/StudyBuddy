using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Repositories.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public byte[] Salt { get; set; }

        public bool IsActive { get; set; }

        public bool IsTeacher { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public Guid? SchoolId { get; set; }

        public School? School { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
