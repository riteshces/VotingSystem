using Microsoft.AspNetCore.Diagnostics;

namespace VotingSystem.API.Exception
{
    public class AddExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotImplementedException)
            {
                var response = new ExceptionResponse()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = exception.Message,
                    Title = "Something went wrong"
                };

                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                return true;
            }
            else if (exception is NotImplementedException)
            {
                var response = new ExceptionResponse()
                {
                    StatusCode = StatusCodes.Status501NotImplemented,
                    Message = exception.Message,
                    Title = "Something went wrong"
                };

                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
                return true;
            }

            return false;
        }

    }

}
