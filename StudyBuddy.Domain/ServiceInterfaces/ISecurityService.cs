namespace StudyBuddy.Domain.ServiceInterfaces
{
    public interface ISecurityService
    {
        string GenerateJwtToken(Guid userId, string role);
    }

}
