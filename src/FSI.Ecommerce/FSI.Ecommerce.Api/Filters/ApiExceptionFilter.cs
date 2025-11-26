using FSI.Ecommerce.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FSI.Ecommerce.Api.Filters
{
    public sealed class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            switch (ex)
            {
                case NotFoundAppException notFound:
                    _logger.LogWarning(ex, "Resource not found: {Resource}", notFound.ResourceName);
                    context.Result = new NotFoundObjectResult(new
                    {
                        error = notFound.Message
                    });
                    break;

                case ValidationAppException validation:
                    _logger.LogWarning(ex, "Validation error");
                    context.Result = new BadRequestObjectResult(new
                    {
                        error = validation.Message,
                        validationErrors = validation.Errors
                    });
                    break;

                default:
                    _logger.LogError(ex, "Unhandled exception");
                    context.Result = new ObjectResult(new
                    {
                        error = "An unexpected error occurred."
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
            }

            context.ExceptionHandled = true;
        }
    }
}
