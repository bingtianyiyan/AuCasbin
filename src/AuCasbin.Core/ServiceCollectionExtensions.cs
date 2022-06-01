using AuCasbin.Core.Auth;
using AuCasbin.Core.Configurations;
using AuCasbin.Core.Db;
using AuCasbin.Core.Filters;
using AuCasbin.Core.Logs;
using AuCasbin.Infrastructure.Configs;
using AuCasbin.Infrastructure.Consts;
using AuCasbin.Infrastructure.Enums;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultService(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers(options =>
            {
                options.Filters.Add<ControllerExceptionFilter>();
                options.Filters.Add<ValidateInputFilter>();
                options.Filters.Add<ControllerLogFilter>();
            }).AddNewtonsoftJson(
                options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //使用驼峰 首字母小写
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }
                );

            services.AddMemoryCache()
                     .AddOptions()
                     .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                ;

            services.AddHttpClient();

            //编码 razor
            services.AddSingleton(System.Text.Encodings.Web.HtmlEncoder.Create(System.Text.Unicode.UnicodeRanges.All));
            return services;
        }

        public static IServiceCollection AddSwaggerServie(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                typeof(ApiVersion).GetEnumNames().ToList().ForEach(version =>
                {
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = "AuCasbin"
                    });
                    //c.OrderActionsBy(o => o.RelativePath);
                });

                options.SchemaFilter<EnumSchemaFilter>();

                options.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });

                options.ResolveConflictingActions(apiDescription => apiDescription.First());
                options.CustomSchemaIds(x => x.FullName);
                options.DocInclusionPredicate((docName, description) => true);

                string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                if (xmlFiles.Length > 0)
                {
                    foreach (var xmlFile in xmlFiles)
                    {
                        options.IncludeXmlComments(xmlFile, true);
                    }
                }

                //添加Jwt验证设置
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });

                //添加设置Token的按钮
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //string xmlPath = Path.Combine(AppContext.BaseDirectory, $"AuCasbinApi.xml");
                //options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection AddDbService(this IServiceCollection services)
        {
            DbConfig dbConfig = ConfigurationManager.GetSection("Db").Get<DbConfig>();
            //添加数据库
            services.AddDbAsync(dbConfig).Wait();

            //数据库配置
            services.AddSingleton(dbConfig);
            //添加IdleBus单例
            var timeSpan = dbConfig.IdleTime > 0 ? TimeSpan.FromMinutes(dbConfig.IdleTime) : TimeSpan.MaxValue;
            IdleBus<IFreeSql> ib = new IdleBus<IFreeSql>(timeSpan);
            services.AddSingleton(ib);
            return services;
        }


        public static IServiceCollection AddJwtService(this IServiceCollection services)
        {
            //权限处理
            services.AddScoped<IPermissionHandler, PermissionHandler>();
            // ClaimType不被更改
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //jwt
            services.TryAddSingleton<IUser, User>();

            var jwtConfig = ConfigurationManager.GetSection("Jwt").Get<JwtConfig>();
            services.TryAddSingleton(jwtConfig);

            //jwt
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), o => { });
            return services;
        }

        public static IServiceCollection AddOtherService(this IServiceCollection services)
        {
            services.AddScoped<ILogHandler, LogHandler>();

            #region Cors 跨域
            services.AddCors(options =>
            {
                options.AddPolicy(AdminConsts.RequestPolicyName, policy =>
                {
                    //var hasOrigins = appConfig.CorUrls?.Length > 0;
                    //if (hasOrigins)
                    //{
                    //    policy.WithOrigins(appConfig.CorUrls);
                    //}
                    //else
                    //{
                    //    policy.AllowAnyOrigin();
                    //}
                    policy.AllowAnyOrigin();
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod();

                    //if (hasOrigins)
                    //{
                    //    policy.AllowCredentials();
                    //}
                });

                //允许任何源访问Api策略，使用时在控制器或者接口上增加特性[EnableCors(AdminConsts.AllowAnyPolicyName)]
                options.AddPolicy(AdminConsts.AllowAnyPolicyName, policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            #endregion Cors 跨域

            #region Mapster 映射配置

            Assembly[] assemblies = DependencyContext.Default.RuntimeLibraries
                .Where(a => a.Name.StartsWith("AuCasbin"))
                .Select(o => Assembly.Load(new AssemblyName(o.Name))).ToArray();
            services.AddScoped<IMapper>(sp => new Mapper());
            TypeAdapterConfig.GlobalSettings.Scan(assemblies);

            #endregion Mapster 映射配置
            return services;
        }
    }
}
