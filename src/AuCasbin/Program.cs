using AuCasbin.Core.Configurations;
using AuCasbin.Core.RegisterModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuCasbinApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            // 将配置传给 Serilog 的提供程序 
                            //.ReadFrom.Configuration(Configuration)
                            .Enrich.With(new DateTimeNowEnricher())
                            .MinimumLevel.Information()//最小记录级别
                            .Enrich.FromLogContext()//记录相关上下文信息 
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//对其他日志进行重写,除此之外,目前框架只有微软自带的日志组件
                                                                                          //.WriteTo.Console()//输出到控制台 //.WriteTo.File

                             .WriteTo.Console()
                             .WriteTo.File("logs/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/log_.log", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                            .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //todo 
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//集成Autofac
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                // 控制器注入
                builder.RegisterModule(new ControllerModule());

                // 单例注入
                builder.RegisterModule(new SingleInstanceModule());

                // 仓储注入
                builder.RegisterModule(new RepositoryModule());

                // 服务注入
                builder.RegisterModule(new ServiceModule());
            })
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
            })
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                // env = "Staging";//启动设置调试值
                Console.WriteLine("env->:" + env ?? "");
                configApp.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, true)
                         .AddJsonFile($"appsettings.{env}.json", optional: true, true)
                        ;
                //configApp.AddApollo(configApp.Build().GetSection("apollo"))
                //         .AddDefault()
                //         .AddNamespace("JobConfig", ConfigFileFormat.Json);
                ConfigurationManager.SetConfiguration(configApp.Build());
                var tt = ConfigurationManager.GetSection("Kafka:Node1:Server");
                Console.WriteLine(tt.Value);
            })//.UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                //logger注入使用Serilog日志 dispose 参数设置为 true 会在程序退出时释放日志对象
                .UseSerilog(dispose: true);
    }

    #region Serilog 相关设置
    class DateTimeNowEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "DateTimeNow", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }

    #endregion
}
