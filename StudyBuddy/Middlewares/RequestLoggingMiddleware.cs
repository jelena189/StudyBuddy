namespace StudyBuddy.WebApi.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLoggingMiddleware> logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var streamCopy = new MemoryStream())
            {
                await context.Request.Body.CopyToAsync(streamCopy);
                streamCopy.Position = 0;

                string body = new StreamReader(streamCopy).ReadToEnd();
                logger.LogInformation($@"Request Method: {context.Request?.Method}
                Request Path: {context.Request?.Path}
                Request Body: {body}");

                context.Request.Body = streamCopy; 
                context.Request.Body.Position = 0;
                await next.Invoke(context);
            }     
        }
    }
}