using ErrorHandlingDll.FixTypes.Enumarions;
using ErrorHandlingDll.Middlewares;
using ErrorHandlingDll.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandlingDll.Configurations
{
  public static class ErrorHandlingDllConfigurator
  {
    public static void InjectServices(IServiceCollection services, IConfiguration configuration)
    {
      string sentryDsn = configuration["Sentry:Dsn"];
      SentrySdk.Init(sentryDsn);
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

    public static void ConfigureAppPipeline(WebApplication app)
    {
      app.UseMiddleware<ErrorHandlingMiddleware>();
    }

  }
}
