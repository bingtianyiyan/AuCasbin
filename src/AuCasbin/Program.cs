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
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())//����Autofac
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                // ������ע��
                builder.RegisterModule(new ControllerModule());

                // ����ע��
                builder.RegisterModule(new SingleInstanceModule());

                // �ִ�ע��
                builder.RegisterModule(new RepositoryModule());

                // ����ע��
                builder.RegisterModule(new ServiceModule());
            })
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
            })
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                // env = "Staging";//�������õ���ֵ
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
