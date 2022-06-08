using ErrorHandlingDll.FixTypes.Enumarions;
using ErrorHandlingDll.ReturnTypes;
using ErrorHandlingDll.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.Middlewares
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
        await HandleErrorResponse(contex);
      }
    }

    private async Task HandleErrorResponse(HttpContext contex)
    {
      ReturnModel<object> response = new();
      response.CreateServerErrorModel();
      contex.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      contex.Response.ContentType = "application/json; charset=utf-8";
      await contex.Response.WriteAsJsonAsync(response);

    }


  }
}
