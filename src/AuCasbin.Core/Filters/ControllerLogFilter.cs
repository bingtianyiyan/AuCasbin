﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using AuCasbin.Core.Attributes;
using AuCasbin.Core.Logs;

namespace AuCasbin.Core.Filters
{
    /// <summary>
    /// 控制器操作日志记录
    /// </summary>
    public class ControllerLogFilter : IAsyncActionFilter
    {
        private readonly ILogHandler _logHandler;

        public ControllerLogFilter(ILogHandler logHandler)
        {
            _logHandler = logHandler;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(NoOprationLogAttribute)))
            {
                return next();
            }

            return _logHandler.LogAsync(context, next);
        }
    }
}