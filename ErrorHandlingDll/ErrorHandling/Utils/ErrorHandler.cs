using System.Net;
using ErrorHandling.ReturnTypes;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ErrorHandling.Utils;

public static class ErrorHandler
{
    public static async Task CreateErrorResponse(this HttpContext contex)
    {
        ReturnModel<object> response = new();
        response.CreateServerErrorModel();
        contex.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        contex.Response.ContentType = "application/json; charset=utf-8";
        await contex.Response.WriteAsJsonAsync(response);

    }
    
    public static  void CreateErrorResponse(this ExceptionContext contex)
    {
        ReturnModel<object> response = new();
        response.CreateServerErrorModel();
        contex.Result = new JsonResult(response){StatusCode = StatusCodes.Status500InternalServerError};
    }
}