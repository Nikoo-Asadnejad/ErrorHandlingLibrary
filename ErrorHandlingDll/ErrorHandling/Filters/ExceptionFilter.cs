using ErrorHandling.FixTypes.Enumarions;
using ErrorHandling.Interfaces;
using ErrorHandling.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using IExceptionFilter = Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter;

namespace ErrorHandling.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILoggerService _logger;
    public ExceptionFilter(ILoggerService logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        _logger.CaptureLogAsync(LogLevel.Error, context.Exception , $"An Error Captured by ErrorHandlingMiddleware  : {context.Exception.Message}");
         context.CreateErrorResponse();
    }
}