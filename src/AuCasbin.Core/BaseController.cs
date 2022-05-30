using AuCasbin.Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AuCasbin.Core
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    //[ValidatePermission]
    [ValidateInput]
    public abstract class BaseController : ControllerBase
    {
    }
}
