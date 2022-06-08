using ErrorHandlingDll.ReturnTypes;
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
   // private readonly HttpContent _context;
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(HttpContext context, RequestDelegate next)
    {
    //  _context = = context;
      _next = next;
    }
    public async Task InvokeAsync(HttpContext contex , RequestDelegate next)
    {
      try
      {
        await next(contex);
      }
      catch (Exception ex)
      {
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
