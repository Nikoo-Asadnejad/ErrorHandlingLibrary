namespace ErrorHandlingDll.ReturnTypes
{
  public static class ReturnMessage
  {
    public const string SuccessMessage = "عملیات با موفقعیت انجام شد";
    public const string NotFoundMessage = "دیتای مورد نظر یافت نشد";
    public const string UnAuthorizedMessage = "برای دسترسی به این بخش باید ابتدا وارد سیستم شوید";
    public const string ServerErrorMessage = "خطای سرور";
    public const string DuplicationErrorMessage = "داده ورودی تکراری است";
    public const string InvalidInputDataErrorMessage = "داده ورودی نا معتبر است";
    public const string BadRequestErrorMessage = "خطایی در دریافت اطلاعات رخ داده است";
  }
}
