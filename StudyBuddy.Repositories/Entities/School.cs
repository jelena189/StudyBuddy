using System.ComponentModel.DataAnnotations;

namespace StudyBuddy.Repositories.Entities
{
    public class School : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}
