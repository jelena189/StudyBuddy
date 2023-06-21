using System.Net;

namespace StudyBuddy.Services.ErrorHandling
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public bool HasResult { get; set; } = false;

        public string GenericMessage { get; set; } = "An exception was thrown.";

        public BaseException(string message) : base(message)
        {

        }
    }
}
