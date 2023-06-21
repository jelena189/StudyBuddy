using System.Net;

namespace StudyBuddy.Services.ErrorHandling
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message)
        {
            HasResult = false;
            GenericMessage = "Unauthorized exception was thrown.";
            StatusCode = HttpStatusCode.Unauthorized;
        }
    }
}
