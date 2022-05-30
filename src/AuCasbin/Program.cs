using AuCasbin.Core.Configurations;
using AuCasbin.Core.RegisterModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
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
            CreateHostBuilder(args).Build().Run();
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
            }).UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
