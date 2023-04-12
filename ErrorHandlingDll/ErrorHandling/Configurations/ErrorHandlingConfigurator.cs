using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHandling.Filters;
using ErrorHandling.FixTypes.Enumarions;
using ErrorHandling.Interfaces;
using ErrorHandling.Middlewares;
using ErrorHandling.Services;

namespace ErrorHandling.Configurations
{
  public static class ErrorHandlingDllConfigurator
  {
    public static void InjectServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddControllers(config =>
      {
        config.Filters.Add(typeof(ExceptionFilter));
      });
      
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
