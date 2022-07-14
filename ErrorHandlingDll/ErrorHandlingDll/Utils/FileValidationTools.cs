using ErrorHandlingDll.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ErrorHandlingDll.FixTypes.FileValidationSettings;

namespace ErrorHandlingDll.Utils
{
  public static class FileValidationTools
  {
    public const int ImageMinimumBytes = 512;
    public static (bool isValid, string message) ValidateFile(IFormFile file,
      FileValidationSettingModel validationSetting)
    {

      if (file == null)
        return (false, FileValidationErrors.NullError);

      if (validationSetting.ValidFormats != null)
        if (!ValidateFormat(file, validationSetting.ValidFormats))
          return (false, FileValidationErrors.InvalidFormatError);

      if (validationSetting.ValidContentTypes != null)
        if (!ValidateContentType(file, validationSetting.ValidContentTypes))
          return (false, FileValidationErrors.InvalidFormatError);

      if (validationSetting.ValidMinSize != null && validationSetting.ValidMaxSize != null)
        if (!ValidateSize(file, (int)validationSetting.ValidMaxSize, (int)validationSetting.ValidMinSize))
          return (false, FileValidationErrors.InvalidSizeError);


      if (validationSetting.Type == FileTypes.Image || validationSetting.Type == FileTypes.Video)
      {
        if (!IsSafe(file))
          return (false, FileValidationErrors.IncludeMaliciousBytesError);

        if (validationSetting.ValidDimensions is not null ||
            validationSetting.MaxDimensions is not null ||
            validationSetting.MinDimensions is not null ||
            validationSetting.Ratio is not null)
          if (!ValidateImageDimensions(file, validationSetting.ValidDimensions, validationSetting.MinDimensions,
              validationSetting.MaxDimensions, validationSetting.Ratio))
            return (false, FileValidationErrors.InvalidDimentionError);
      }


      return (true, "Ok");
    }

    //Checks if the file contains malicious scripts 
    private static bool IsSafe(IFormFile file)
    {
      if (!file.OpenReadStream().CanRead)
        return false;

      try
      {
        using var bitmap = new Bitmap(file.OpenReadStream());
      }
      catch (Exception)
      {
        return false;
      }
      finally
      {
        file.OpenReadStream().Position = 0;
      }

      byte[] buffer = new byte[ImageMinimumBytes];
      file.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
      string content = Encoding.UTF8.GetString(buffer);
      return !Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
              RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline);

    }
    private static bool ValidateImageDimensions(IFormFile file, List<KeyValuePair<int, int>> dimensions,
        KeyValuePair<int, int>? minDimensions, KeyValuePair<int, int>? maxDimensions, KeyValuePair<int, int>? ratio)
    {
      Image image = Image.FromStream(file.OpenReadStream());
      int width = image.Width;
      int height = image.Height;
      int imgRatio = (int)height / width;

      if (minDimensions is not null)
        if (width < minDimensions.Value.Key || height < minDimensions.Value.Value)
          return false;

      if (maxDimensions is not null)
        if (width > maxDimensions.Value.Key || height > maxDimensions.Value.Value)
          return false;


      if (dimensions is not null && dimensions.Count() > 0)
        if (!dimensions.Any(dm => dm.Key == width && dm.Value == height))
          return false;

      if (ratio is not null)
        if (imgRatio != ratio.Value.Key)
          return false;

      return true;
    }
    private static bool ValidateFormat(IFormFile file, List<string> validFormats)
    {
      string fileFormat = Path.GetExtension(file.FileName).ToLower();
      return validFormats.Any(f => f == fileFormat);
    }
    private static bool ValidateContentType(IFormFile file, List<string> validContentTypes)
    {
      string fileContentType = file.ContentType.ToLower();
      return validContentTypes.Any(cp => cp == fileContentType);
    }
    private static bool ValidateSize(IFormFile file, int maxSize, int minSize)
    => file.Length < maxSize || file.Length > minSize;

  }
}
