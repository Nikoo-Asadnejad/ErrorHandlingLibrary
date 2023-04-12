using Sentry;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandling.FixTypes.Enumarions;
using ErrorHandling.Interfaces;

namespace ErrorHandling.Services
{
  public class SerilogLoggerService : ILoggerService
  {
    public async Task CaptureLogAsync(LogLevel level, string message, Dictionary<string, string> tags = null)
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

    public async Task CaptureLogAsync(LogLevel level, Exception exception, string message = null, Dictionary<string, string> tags = null)
    {

        switch (level)
        {
          case LogLevel.Debug: Log.Debug<Exception>(message, exception); break;
          case LogLevel.Error: Log.Error<Exception>(message , exception); break;
          case LogLevel.Info: Log.Information<Exception>(message , exception); break;
          case LogLevel.Fatal: Log.Fatal<Exception>(message , exception); break;
          case LogLevel.Warning: Log.Warning<Exception>(message, exception); break;

        }
    
      
    }
  }
}
