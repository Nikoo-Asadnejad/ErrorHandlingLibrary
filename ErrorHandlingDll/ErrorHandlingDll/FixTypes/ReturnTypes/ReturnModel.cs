
using System.Net;

namespace ErrorHandlingDll.ReturnTypes
{
  public class ReturnModel<T>
  {
    public HttpStatusCode HttpStatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public string DataTitle { get; set; }

    public ReturnModel()
    {

    }

    public ReturnModel(string title , T data , HttpStatusCode statusCode , string message)
    {
      Data = data;
      DataTitle = title;
      HttpStatusCode = statusCode;
      Message = message;
    }


    public ReturnModel<T> CreateSuccessModel(T data ,string title = null, string message = null)
    { 
      this.Data = data;
      this.HttpStatusCode = HttpStatusCode.OK;
      this.DataTitle = title;
      this.Message = message == null ? ReturnMessage.SuccessMessage : message;
      return this;
    }

    public ReturnModel<T> CreateNotFoundModel(string title = null,string message = null)
    {
      this.HttpStatusCode = HttpStatusCode.NotFound;
      this.DataTitle = title;
      this.Message = message == null ? ReturnMessage.NotFoundMessage : message;
      return this;
    }

    public ReturnModel<T> CreateServerErrorModel(string title = null, string message = null)
    {
      this.HttpStatusCode = HttpStatusCode.InternalServerError;
      this.DataTitle = title;
      this.Message = message == null ? ReturnMessage.ServerErrorMessage : message;
      return this;
    }

    public ReturnModel<T> CreateBadRequestModel(string title = null, string message = null)
    {
      this.HttpStatusCode = HttpStatusCode.BadRequest;
      this.DataTitle = title;
      this.Message = message == null ? ReturnMessage.ServerErrorMessage : message;
      return this;
    }


    public ReturnModel<T> CreateInvalidInputErrorModel(string title = null, string message = null)
    {
      this.HttpStatusCode = HttpStatusCode.BadRequest;
      this.DataTitle = title;
      this.Message = message == null ? ReturnMessage.InvalidInputDataErrorMessage : message;
      return this;
    }


    public ReturnModel<T> CreateDuplicatedErrorModel(string title = null, string message = null)
    {
      this.HttpStatusCode = HttpStatusCode.BadRequest;
      this.DataTitle = title;
      this.Message = message == null ? ReturnMessage.DuplicationErrorMessage : message;
      return this;
    }

  }
}
