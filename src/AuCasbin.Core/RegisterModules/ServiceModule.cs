using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;
using Microsoft.Extensions.DependencyModel;

namespace AuCasbin.Core.RegisterModules
{
    /// <summary>
    /// 服务注入
    /// </summary>
    public class ServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //事务拦截
            var interceptorServiceTypes = new List<Type>();
            //if (_appConfig.Aop.Transaction)
            //{
            //    builder.RegisterType<TransactionInterceptor>();
            //    builder.RegisterType<TransactionAsyncInterceptor>();
            //    interceptorServiceTypes.Add(typeof(TransactionInterceptor));
            //}

            //服务
            Assembly[] assemblies = DependencyContext.Default.RuntimeLibraries
                .Where(a => a.Name == "AuCasbin.DomainService")
                .Select(o => Assembly.Load(new AssemblyName(o.Name))).ToArray();

            //服务接口实例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(a => a.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .PropertiesAutowired()// 属性注入
            .InterceptedBy(interceptorServiceTypes.ToArray())
            .EnableInterfaceInterceptors();

            //服务实例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(a => a.Name.EndsWith("Service"))
            .InstancePerLifetimeScope()
            .PropertiesAutowired()// 属性注入
            .InterceptedBy(interceptorServiceTypes.ToArray())
            .EnableClassInterceptors();
        }
    }
}
