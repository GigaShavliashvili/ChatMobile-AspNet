using Azure.Identity;
using Newtonsoft.Json;
using System.Net;

namespace chatmobile.MiddleWares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            //using var scope = serviceProvider.CreateScope();
            //var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            try
            {
                await _next(context);
            }
            // catch (AuthException ex)
            // {
            //     if (!string.IsNullOrEmpty(ex.UserName))
            //     {
            //         using var scope = serviceProvider.CreateScope();
            //         var dataContext = scope.ServiceProvider.GetRequiredService<ICallAppUnitOfWork>();
            //         var logUserActivity = scope.ServiceProvider.GetRequiredService<ILogUserActivity>();
            //         await ex.RunAsyncAction(dataContext, logUserActivity, default, ex.Message);
            //     }

            //     await HandleExceptionAsync(context, ex, (int)HttpStatusCode.BadRequest, serviceProvider);
            // }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.BadRequest, serviceProvider);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, 401, serviceProvider);
            }
            catch (AuthenticationFailedException ex) // Replace with the actual exception type
            {
                await HandleExceptionAsync(context, ex, 401, serviceProvider);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.InternalServerError, serviceProvider);
            }
        }


        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode,
            IServiceProvider serviceProvider)
        {
            // Set the response status code and return an error response
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var errorResponse = ex.InnerException?.Message ?? ex.Message;
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            //return
            await context.Response.WriteAsync(jsonErrorResponse);
        }
    }
}