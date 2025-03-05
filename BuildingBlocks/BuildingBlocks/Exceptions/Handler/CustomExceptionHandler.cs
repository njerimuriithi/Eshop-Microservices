
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler>logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext Context, Exception exception, CancellationToken cancellationToken)
        {
         logger.LogError("Error Message",exception.Message,DateTime.UtcNow);  
            (string Detail,string Title,int StatusCode) details = exception switch
            {
                InvalidServerException =>
                (
                exception.Message,
                exception.GetType().Name,
                Context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                exception.Message,
                exception.GetType().Name,
                Context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException =>
                 (
                 exception.Message,
                 exception.GetType().Name,
                 Context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                exception.Message,
                exception.GetType().Name,
                Context.Response.StatusCode = StatusCodes.Status404NotFound
                ),_ =>
                (
                exception.Message,
                exception.GetType().Name,
                Context.Response.StatusCode = StatusCodes.Status500InternalServerError

                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Detail,
                Instance = Context.Request.Path
            };

            problemDetails.Extensions.Add("traceId",Context.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors",validationException.Message);
            } 
            
            await Context.Response.WriteAsJsonAsync(problemDetails,cancellationToken:cancellationToken);
            return true;


        }
    }
}
