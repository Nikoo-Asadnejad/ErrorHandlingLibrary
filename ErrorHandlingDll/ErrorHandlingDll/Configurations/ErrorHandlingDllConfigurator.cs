using ErrorHandlingDll.FixTypes.Enumarions;
using ErrorHandlingDll.Services;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.Configurations
{
  public static class ErrorHandlingDllConfigurator
  {
    public static void InjectServices(IServiceCollection services)
    {
      services.AddTransient<SentryLoggerService>();
      services.AddTransient<SerilogLoggerService>();
      services.AddTransient<Func<LoggerIds, ILoggerService>>(serviceProvider => LoggerId =>
      {
        switch (LoggerId)
        {
          case LoggerIds.Sentry: return serviceProvider.GetService<SentryLoggerService>();
          case LoggerIds.Serilog: return serviceProvider.GetService<SerilogLoggerService>();
          default: return serviceProvider.GetService<SentryLoggerService>();
        }

      });
    }

    //public static void ConfigureAppPipeline(WebApplication app)
    //{

    //}

  }
}
