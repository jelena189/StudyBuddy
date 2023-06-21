namespace StudyBuddy.Domain.Models
{
    public class UserSignUp
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsTeacher { get; set; }

        public Guid? SchoolId { get; set; }
    }
}
