using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmicApi.Application.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result)
            => result.IsSuccess ? 
                new OkObjectResult(result) : result.ToHttpNonSuccessResult();
        
        public static IActionResult ToActionResult<T>(this Result<T> result)
            => result.IsSuccess ? 
                new OkObjectResult(result.Value) : result.ToHttpNonSuccessResult();

        private static IActionResult ToHttpNonSuccessResult(this IResult result)
        {
            var errors = result.Errors.ToList();
            var objectResult = new ObjectResult(errors);
            
            switch (result.Status)
            {
                case ResultStatus.Invalid:
                    objectResult.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                
                case ResultStatus.NotFound:
                    objectResult.StatusCode = StatusCodes.Status404NotFound;
                    break;
                
                case ResultStatus.Unauthorized:
                    objectResult.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                
                case ResultStatus.Forbidden:
                    objectResult.StatusCode = StatusCodes.Status403Forbidden;
                    break;
                
                default:
                    objectResult.StatusCode = StatusCodes.Status400BadRequest;
                    break;
            }

            return objectResult;
        }
    }
}
