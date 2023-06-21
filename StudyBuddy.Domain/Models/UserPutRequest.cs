﻿namespace StudyBuddy.Domain.Models
{
    public class UserPutRequest
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }

        public string Role { get; set; }
    }

}
