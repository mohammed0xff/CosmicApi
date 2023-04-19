using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace CosmicApi.Application.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult? ToActionResult<T>(this Result<T> result)
        {
            if (result == null) return new BadRequestResult();

            switch (result.Status)
            {
                case ResultStatus.Ok:
                    return new OkObjectResult(result.Value);

                case ResultStatus.Error:
                    var errors = result.Errors.ToList();
                    var objectResult = new ObjectResult(errors);
                    objectResult.StatusCode = 400;
                    return objectResult;

                case ResultStatus.Invalid:
                    return new BadRequestObjectResult(result.ValidationErrors);

                case ResultStatus.NotFound:
                    return new NotFoundResult();

                case ResultStatus.Unauthorized:
                    return new UnauthorizedResult();

                case ResultStatus.Forbidden:
                    return new ForbidResult();

                default:
                    return new StatusCodeResult(500);
            }

        }
    }
}
