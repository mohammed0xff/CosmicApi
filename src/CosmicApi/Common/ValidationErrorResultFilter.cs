using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

using CosmicApi.Application.Common;
using CosmicApi.Application.Common.Handlers;


namespace CosmicApi.Api.Common
{
    public class ValidationErrorResultFilter : IAsyncResultFilter
    {
        private readonly ValidationErrorHandler _errorHandler;

        public ValidationErrorResultFilter(INotificationHandler<ValidationError> errorHandler)
        {
            _errorHandler = (ValidationErrorHandler)errorHandler;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_errorHandler.HasErrors)
            {
                var errors = _errorHandler.GetErrors();

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await context.HttpContext.Response.WriteAsJsonAsync(errors)
                    .ConfigureAwait(false);

                // short circuit 
                return;
            }

            await next().ConfigureAwait(false);
        }
    }
}