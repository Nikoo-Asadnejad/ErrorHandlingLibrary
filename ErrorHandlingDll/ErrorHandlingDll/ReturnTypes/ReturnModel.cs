using BankMicroservice.Persistances.Enumerations;
using System.Net;

namespace BankMicroservice.Persistances.ReturnTypes
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

    public ReturnModel(string title,T? data ,
      ReturnModelTypes? type = null,HttpStatusCode? statusCode = null,
      string message = null)
    {
      DataTitle = title;
      Data = data;
      switch (type)
        {
        case ReturnModelTypes.Success:
          {
            Data = data;
            HttpStatusCode = HttpStatusCode.OK;
            Message = message == null ? ReturnMessage.SuccessMessage : message;
            
          }
          break;
        case ReturnModelTypes.NotFound:
          {
            HttpStatusCode = HttpStatusCode.NotFound;
            Message = message == null ? ReturnMessage.NotFoundMessage : message;
          }
          break;
        case ReturnModelTypes.InternalServerError:
          {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Message = message == null ? ReturnMessage.ServerErrorMessage : message;
          }
          break;
        case ReturnModelTypes.BadRequest:
          {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Message = message == null ? ReturnMessage.BadRequestErrorMessage : message;

          }
          break;
        case ReturnModelTypes.InvalidInput:
          {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Message = message != null ? ReturnMessage.InvalidInputDataErrorMessage : message;
          }
          break;
        case ReturnModelTypes.DuplicationError:
          {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Message = message == null ? ReturnMessage.DuplicationErrorMessage : message;
          }
          break;
        default:
          {
            HttpStatusCode = (HttpStatusCode)statusCode;
            Message = message;
          }
          break;
      }
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
