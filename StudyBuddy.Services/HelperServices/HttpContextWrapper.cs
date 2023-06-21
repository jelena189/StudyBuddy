using Microsoft.AspNetCore.Http;
using StudyBuddy.Domain.Resources;
using StudyBuddy.Domain.ServiceInterfaces;
using System.Security.Claims;

namespace StudyBuddy.Services.Services.HelperServices
{
    public class HttpContextWrapper : IHttpContextWrapper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextWrapper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool CanAccess(Guid currentUserId, Guid userId)
        {
            if (!httpContextAccessor.HttpContext.User.IsInRole(RoleNames.Administrator) && currentUserId != userId)
            {
                return false;
            }

            return true;
        }

        public Guid GetCurrentUsersId()
        {
            return Guid.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
