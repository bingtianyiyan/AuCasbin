using AuCasbin.Core.Attributes;
using Autofac;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace AuCasbin.Core.RegisterModules
{
    /// <summary>
    /// 单例注入
    /// </summary>
    public class SingleInstanceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            // 获得要注入的程序集
            Assembly[] assemblies = DependencyContext.Default.RuntimeLibraries
                .Where(a => a.Name == "AuCasbin.Core")
                .Select(o => Assembly.Load(new AssemblyName(o.Name))).ToArray();

            //无接口注入单例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<SingleInstanceAttribute>() != null)
            .SingleInstance()
            .PropertiesAutowired();

            //有接口注入单例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<SingleInstanceAttribute>() != null)
            .AsImplementedInterfaces()
            .SingleInstance()
            .PropertiesAutowired();
        }
    }
}
