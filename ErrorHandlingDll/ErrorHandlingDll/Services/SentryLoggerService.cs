using ErrorHandlingDll.FixTypes.Enumarions;
using ErrorHandlingDll.Interfaces;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.Services
{
  public class SentryLoggerService : ILoggerService
  {
    public async Task CaptureLogAsync(LogLevel level, string message, Dictionary<string, string> tags = null)
    {
      SentrySdk.CaptureMessage(message, (SentryLevel)level);

      if (tags is not null && tags?.Count() > 0)
        AddTags(tags);
    }

    public async Task CaptureLogAsync(LogLevel level, Exception exception, string message = null, Dictionary<string, string> tags = null)
    {

      SentrySdk.CaptureException(exception);

      if (message is not null)
        SentrySdk.CaptureMessage(message, (SentryLevel)level);

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
