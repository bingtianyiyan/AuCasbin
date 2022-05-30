using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.Core.Configurations
{
    public class ConfigurationManager
    {
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfigurationSection GetSection(string secetionName)
        {
            if (Configuration == null)
            {
                return null;
            }
            return Configuration.GetSection(secetionName);
        }

        /// <summary>
        /// 获取配置连接的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            if (Configuration == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            return Configuration.GetConnectionString(key);
        }

        /// <summary>
        /// 获取配置对应的值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">配置的key</param>
        /// <returns>配置对应的值</returns>
        public static T GetValue<T>(string key)
        {
            if (Configuration == null)
            {
                return default;
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                return default;
            }
            return Configuration.GetValue<T>(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">值得类型</typeparam>
        /// <returns>配置值</returns>
        public static T Get<T>()
        {
            if (Configuration == null)
            {
                return default;
            }
            return Configuration.Get<T>();
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">配置的key</param>
        /// <returns>配置值</returns>
        public static string GetValue(string key)
        {
            if (Configuration == null)
            {
                return null;
            }
            return Configuration[key];
        }
    }
}
