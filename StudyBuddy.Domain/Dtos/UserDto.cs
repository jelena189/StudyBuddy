namespace StudyBuddy.Domain.Dtos
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public byte[] Salt { get; set; }

        public bool IsActive { get; set; }

        public bool IsTeacher { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public Guid? SchoolId { get; set; }
    }
}
