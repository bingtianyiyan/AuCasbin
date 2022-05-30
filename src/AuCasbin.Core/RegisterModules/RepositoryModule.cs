using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;
using Microsoft.Extensions.DependencyModel;
using AuCasbin.Core.Repositories;

namespace AuCasbin.Core.RegisterModules
{
    /// <summary>
    /// 仓储层注入/根据项目进行修改名称
    /// </summary>
    public class RepositoryModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //仓储
            Assembly[] assemblies = DependencyContext.Default.RuntimeLibraries
                .Where(a => a.Name == "AuCasbin.Repository")
                .Select(o => Assembly.Load(new AssemblyName(o.Name))).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
            .Where(a => a.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .PropertiesAutowired();// 属性注入

            //泛型注入
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepositoryBase<,>)).InstancePerLifetimeScope();
        }
    }
}
