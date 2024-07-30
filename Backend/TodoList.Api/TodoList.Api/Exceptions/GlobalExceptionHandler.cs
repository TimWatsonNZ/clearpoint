
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TodoList.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        /*
         *  Wanted to handle validaton exceptions but found they are not caught because they aren't
         *  considered an exception. 
         */
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "An unhandled error occurred",
                Detail = exception.Message
            };
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //  Wanted to use switch with pattern matching but this doesn't work with exceptions sadly.
            if (exception is EntityAlreadyExistsException)
            {
                _logger.LogWarning("Entity already exists, error message: {error}", exception.Message);
                problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = exception.GetType().Name,
                    Title = "Entity Uniqueness exception",
                    Detail = "Description must be unique",
                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException)
            {
                _logger.LogWarning("Entity not found error: {error}", exception.Message);
                problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = exception.GetType().Name,
                    Title = "Entity not found",
                    Detail = "",
                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await httpContext
                .Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
