using Microsoft.Extensions.Logging;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.Services
{
  public class LoggerService : ILoggerService
  {
    public async Task CaptureLogAsync(SentryLevel level, string message, Dictionary<string, string> tags = null)
    {
      SentrySdk.CaptureMessage(message, level);

      if (tags is not null && tags?.Count() > 0)
        AddTags(tags);
    }

    public async Task CaptureLogAsync(SentryLevel level, Exception exception, string message = null, Dictionary<string, string> tags = null)
    {

      SentrySdk.CaptureException(exception);

      if (message is not null)
        SentrySdk.CaptureMessage(message, level);

      if (tags is not null && tags?.Count() > 0)
        AddTags(tags);
    }

    private void AddTags(Dictionary<string, string> tags)
    {
      SentrySdk.ConfigureScope(scope =>
      {

        scope.SetTags(tags);

      });
    }
  }
}
