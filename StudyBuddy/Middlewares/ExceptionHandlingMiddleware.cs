using Newtonsoft.Json;
using StudyBuddy.Services.ErrorHandling;

namespace StudyBuddy.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            string result = string.Empty;
            BaseException exception = new BaseException(ex.Message);

            if (ex is FluentValidation.ValidationException fluentValidationEx)
            {
                result = JsonConvert.SerializeObject(new { errors = fluentValidationEx.Errors.Select(e => new { 
                        PropertyName = e.PropertyName, 
                        ErrorMessage = e.ErrorMessage 
                    })
                });
                exception = new ValidationException(result);
            }
            else if (ex is BaseException baseEx)
            {
                result = baseEx.HasResult ? JsonConvert.SerializeObject(new { error = ex.Message }) : string.Empty;
                exception = baseEx;
            }

            logger.LogError(ex, exception.GenericMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
