namespace StudyBuddy.Domain.ServiceInterfaces
{
    public interface IHttpContextWrapper
    {
        bool CanAccess(Guid currentUserId, Guid userId);

        Guid GetCurrentUsersId();
    }
}
