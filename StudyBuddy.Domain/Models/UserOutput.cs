namespace StudyBuddy.Domain.Models
{
    public class UserOutput
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public string FullName { get; set; }
    }
}
