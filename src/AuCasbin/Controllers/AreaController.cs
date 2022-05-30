using AuCasbin.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuCasbinApi.Controllers
{
    /// <summary>
    /// 域控制器
    /// </summary>
    [Area("Admin")]
    public abstract class AreaController : BaseController
    {
    }
}
