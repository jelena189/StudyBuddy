using System.Net;

namespace StudyBuddy.Services.ErrorHandling
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
            HasResult = false;
            GenericMessage = "NotFound exception was thrown.";
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
