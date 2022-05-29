using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebServiceCaller.Application.System;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Persistence.Context;

namespace WebServiceCall.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                #region SeriLog

                var seriLogSetting = new SeriLogSetting();
                config.GetSection("SeriLogSetting").Bind(seriLogSetting);

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .MinimumLevel.Information()
                    .WriteTo.Seq(seriLogSetting.Address)
                    .CreateLogger();

                Log.Information(" SeriLog Initialized on {Address} ... ", seriLogSetting.Address);

                #endregion SeriLog

                #region SeedData

                var context = services.GetRequiredService<WebServiceNotificationContext>();
                await context.Database.EnsureCreatedAsync();

                var mediator = services.GetRequiredService<IMediator>();
                await mediator.Send(new SampleSeedDataCommand(), CancellationToken.None);

                #endregion SeedData

                await host.RunAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}