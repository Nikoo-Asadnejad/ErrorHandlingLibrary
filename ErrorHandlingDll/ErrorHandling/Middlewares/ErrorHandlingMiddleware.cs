using ErrorHandling.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ErrorHandling.FixTypes.Enumarions;
using ErrorHandling.Interfaces;
using ErrorHandling.ReturnTypes;
using ErrorHandling.Utils;

namespace ErrorHandling.Middlewares
{
  public class ErrorHandlingMiddleware : IMiddleware
  {

    private readonly ILoggerService _loggerService;
    public ErrorHandlingMiddleware(ILoggerService loggerService)
    {
      _loggerService = loggerService;
    }
    public async Task InvokeAsync(HttpContext contex , RequestDelegate next)
    {
      try
      {
        await next(contex);
      }
      catch (Exception ex)
      {
        await _loggerService.CaptureLogAsync(LogLevel.Error, ex , $"An Error Captured by ErrorHandlingMiddleware  : {ex.Message}");
        await contex.CreateErrorResponse();
      }
    }

   


  }
}
