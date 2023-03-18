using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace CosmicApi.Application.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult? ToActionResult(this Result result)
        {
            if (result == null) return new BadRequestResult();
            return result.IsSuccess ? 
                new OkObjectResult(result) : result.ToHttpNonSuccessResult();
        }

        public static IActionResult? ToActionResult<T>(this Result<T> result)
        {
            if (result == null) return new BadRequestResult();
            return result.IsSuccess ?
                new OkObjectResult(result.Value) : result.ToHttpNonSuccessResult();
        }

        private static IActionResult ToHttpNonSuccessResult(this IResult result)
        {
            var errors = result.Errors.ToList();
            var objectResult = new ObjectResult(errors);
            switch (result.Status)
            {
                case ResultStatus.Error:
                    objectResult.StatusCode = 400;
                    break;

                case ResultStatus.Invalid:
                    var ValidtionErrors = result.ValidationErrors.ToList();
                    List<string> ValidtionErrorsStrings = new ();
                    foreach ( var error in ValidtionErrors )
                        ValidtionErrorsStrings.Add(error.ErrorMessage);
                    return new BadRequestObjectResult(ValidtionErrorsStrings);
                
                case ResultStatus.NotFound:
                    return new NotFoundObjectResult(errors);
                
                case ResultStatus.Unauthorized:
                    return new UnauthorizedObjectResult(errors);
                
                case ResultStatus.Forbidden:
                    return new ObjectResult(errors) { StatusCode = 403 };
                
                default:
                    break;
            }

            return objectResult;
        }
    }
}
