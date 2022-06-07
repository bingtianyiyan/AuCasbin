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
                            // �����ô��� Serilog ���ṩ���� 
                            //.ReadFrom.Configuration(Configuration)
                            .Enrich.With(new DateTimeNowEnricher())
                            .MinimumLevel.Information()//��С��¼����
                            .Enrich.FromLogContext()//��¼�����������Ϣ 
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
                                                                                          //.WriteTo.Console()//���������̨ //.WriteTo.File

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
            })//.UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                //loggerע��ʹ��Serilog��־ dispose ��������Ϊ true ���ڳ����˳�ʱ�ͷ���־����
                .UseSerilog(dispose: true);
    }

    #region Serilog �������
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
