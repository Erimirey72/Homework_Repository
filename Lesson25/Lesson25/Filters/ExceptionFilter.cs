using Lesson25.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using BusinessLogic;

namespace Lesson25.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HomeController> _logger;

        public ExceptionFilter(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public void OnException(ExceptionContext context)
        {
            var request = context.HttpContext.Request;

            _logger.Log(LogLevel.Error, context.Exception, $"Error happened during request {request.Path}...");
        }
    }
}
