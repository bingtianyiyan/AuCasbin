using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using FreeSql;
using FreeSql.Internal.CommonProvider;
using AuCasbin.Infrastructure.Configs;

namespace AuCasbin.Core.Db
{
    public static class DBServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        public async static Task AddDbAsync(this IServiceCollection services, DbConfig dbConfig)
        {
            services.AddScoped<DbUnitOfWorkManager>();
            #region FreeSql

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
            //        //Console.WriteLine($"{cmd.CommandText}\n{traceLog}\r\n");
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

            #endregion FreeSql

            services.AddSingleton(fsql);

            ////导入多数据库
            //if (null != dbConfig.Dbs)
            //{
            //    foreach (var multiDb in dbConfig.Dbs)
            //    {
            //        switch (multiDb.Name)
            //        {
            //            case nameof(MySqlDb):
            //                var mdb = CreateMultiDbBuilder(multiDb).Build<MySqlDb>();
            //                services.AddSingleton(mdb);
            //                break;

            //            default:
            //                break;
            //        }
            //    }
            //}
           await Task.CompletedTask;
        }

        ///// <summary>
        ///// 创建多数据库构建器
        ///// </summary>
        ///// <param name="multiDb"></param>
        ///// <returns></returns>
        //private static FreeSqlBuilder CreateMultiDbBuilder(MultiDb multiDb)
        //{
        //    return new FreeSqlBuilder()
        //    .UseConnectionString(multiDb.Type, multiDb.ConnectionString)
        //    .UseAutoSyncStructure(false)
        //    .UseLazyLoading(false)
        //    .UseNoneCommandParameter(true);
        //}
    }
}