using FluentValidation.Results;
using System.Net;

namespace StudyBuddy.Services.ErrorHandling
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message) : base(message)
        {
            HasResult = true;
            GenericMessage = "Validation exception was thrown.";
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
