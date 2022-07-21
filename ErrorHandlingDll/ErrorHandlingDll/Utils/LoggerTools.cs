using ErrorHandlingDll.FixTypes.Enumarions;
using Sentry;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.Utils
{
  public static class SentryLoggerTools
  {
    public static async Task CaptureLogAsync(LogLevel level, string message, Dictionary<string, string> tags = null)
    {
      SentrySdk.CaptureMessage(message, (SentryLevel)level);

      if (tags is not null && tags?.Count() > 0)
        AddTags(tags);
    }

    public static async Task CaptureLogAsync(LogLevel level, Exception exception, string message = null, Dictionary<string, string> tags = null)
    {

      SentrySdk.CaptureException(exception);

      if (message is not null)
        SentrySdk.CaptureMessage(message, (SentryLevel)level);

      if (tags is not null && tags?.Count() > 0)
        AddTags(tags);
    }

    private static void AddTags(Dictionary<string, string> tags)
    {
      SentrySdk.ConfigureScope(scope =>
      {

        scope.SetTags(tags);

      });
    }
  }


  public static class SeriLogLoggerTools
  {
    public static async Task CaptureLogAsync(LogLevel level, string message, Dictionary<string, string> tags = null)
    {

      switch (level)
      {
        case LogLevel.Debug: Log.Debug(message); break;
        case LogLevel.Error: Log.Error(message); break;
        case LogLevel.Info: Log.Information(message); break;
        case LogLevel.Fatal: Log.Fatal(message); break;
        case LogLevel.Warning: Log.Warning(message); break;

      }

    }

    public static async Task CaptureLogAsync(LogLevel level, Exception exception, string message = null, Dictionary<string, string> tags = null)
    {

      switch (level)
      {
        case LogLevel.Debug: Log.Debug<Exception>(message, exception); break;
        case LogLevel.Error: Log.Error<Exception>(message, exception); break;
        case LogLevel.Info: Log.Information<Exception>(message, exception); break;
        case LogLevel.Fatal: Log.Fatal<Exception>(message, exception); break;
        case LogLevel.Warning: Log.Warning<Exception>(message, exception); break;

      }


    }
  }
}
