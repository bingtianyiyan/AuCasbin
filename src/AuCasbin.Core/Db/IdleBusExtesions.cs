using Microsoft.Extensions.DependencyInjection;
using System;
using FreeSql;
using FreeSql.Internal.CommonProvider;
using AuCasbin.Infrastructure.Configs;

namespace AuCasbin.Core.Db
{
    public static class IdleBusExtesions
    {
        /// <summary>
        /// 创建FreeSql实例
        /// </summary>
        /// <param name="user"></param>
        /// <param name="appConfig"></param>
        /// <param name="dbConfig"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private static IFreeSql CreateFreeSql(DbConfig dbConfig)
        {
            var freeSqlBuilder = new FreeSqlBuilder()
            .UseConnectionString(dbConfig.Type, dbConfig.ConnectionString)
            .UseAutoSyncStructure(false)
            .UseLazyLoading(false)
            .UseNoneCommandParameter(true);

            #region 监听所有命令

            //if (dbConfig.MonitorCommand)
            //{
            //    freeSqlBuilder.UseMonitorCommand(cmd => { }, (cmd, traceLog) =>
            //    {
            //        Console.WriteLine($"{cmd.CommandText}\r\n");
            //    });
            //}

            #endregion 监听所有命令

            var fsql = freeSqlBuilder.Build();

            #region 监听Curd操作

            //if (dbConfig.Curd)
            //{
            //    fsql.Aop.CurdBefore += (s, e) =>
            //    {
            //        if (appConfig.MiniProfiler)
            //        {
            //            MiniProfiler.Current.CustomTiming("CurdBefore", e.Sql);
            //        }
            //        Console.WriteLine($"{e.Sql}\r\n");
            //    };
            //    fsql.Aop.CurdAfter += (s, e) =>
            //    {
            //        if (appConfig.MiniProfiler)
            //        {
            //            MiniProfiler.Current.CustomTiming("CurdAfter", $"{e.ElapsedMilliseconds}");
            //        }
            //        Console.WriteLine($"{e.Sql}\r\n");
            //    };
            //}

            #endregion 监听Curd操作

            return fsql;
        }

        /// <summary>
        /// 获得FreeSql实例
        /// </summary>
        /// <param name="ib"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IFreeSql GetFreeSql(this IdleBus<IFreeSql> ib, IServiceProvider serviceProvider)
        {
            var freeSql = serviceProvider.GetRequiredService<IFreeSql>();
            return freeSql;
        }
    }
}