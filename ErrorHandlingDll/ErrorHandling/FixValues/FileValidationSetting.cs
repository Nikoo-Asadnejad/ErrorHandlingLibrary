using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling.FixTypes
{
 
    public readonly struct FileValidationSettings
    {
      public readonly struct FileValidationErrors
      {
        public const string NullError = "فایل وارد نشده است";
        public const string InvalidFormatError = "فرمت ورودی صحیح نیست";
        public const string InvalidSizeError = "حجم فایل ورودی صحیح نیست";
        public const string InvalidDimentionError = "اندازه های فایل ورودی صحیح نیست";
        public const string IncludeMaliciousBytesError = "فایل مخرب است";

      }
      public readonly struct ImageFormats
      {
        public const string Jpeg = ".jpeg";
        public const string Jpg = ".jpg";
        public const string Png = ".png";
        public const string Webp = ".webp";
      }
      public readonly struct ImageContentMediaTypes
      {
        public const string Jpg = "image/jpg";
        public const string Jpeg = "image/jpeg";
        public const string PJpeg = "image/pjpeg";
        public const string Png = "image/png";
        public const string XPng = "image/x-png";
        public const string Webp = "image/WebP";
      }
      public readonly struct FileTypes
      {
        public const string Image = "Image";
        public const string Video = "Video";
      }

    }
  }

