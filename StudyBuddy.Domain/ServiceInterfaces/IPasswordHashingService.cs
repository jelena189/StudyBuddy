namespace StudyBuddy.Domain.EncryptionInterfaces
{
    public interface IPasswordHashingService
    {
        public string HashPassword(string password, byte[] salt);

        public bool VerifyPassword(string password, string hashedPassword, byte[] salt);

        public byte[] GenerateSalt();
    }
}
