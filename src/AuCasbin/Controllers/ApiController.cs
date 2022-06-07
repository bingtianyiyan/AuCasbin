using AuCasbin.Core.Attributes;
using AuCasbin.DomainService.Admin;
using AuCasbin.Infrastructure.Api;
using AuCasbin.Infrastructure.Enums;
using AuCasbin.TransData.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuCasbinApi.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
   // [VersionRouteAttribute(ApiVersion.V1)]
    public class ApiController : AreaController
    {
        private readonly IApiDomainService _apiDomainService;

        public ApiController(IApiDomainService apiDomainService)
        {
            _apiDomainService = apiDomainService;
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
       public async Task<IResultOutput> GetAsync(long id)
        {
            return await _apiDomainService.GetAsync(id);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultOutput> GetListAsync(string key)
        {
            return await _apiDomainService.GetListAsync(key);
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> GetPageAsync([FromBody]PageInput<ApiGetPageDto> input)
        {
            return await _apiDomainService.GetPageAsync(input);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
       public async Task<IResultOutput> AddAsync(ApiAddInput input)
        {
            return await _apiDomainService.AddAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       [HttpPost]
       public async Task<IResultOutput> UpdateAsync(ApiUpdateInput input)
        {
            return await _apiDomainService.UpdateAsync(input);
        }

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       [HttpPost]
       public async Task<IResultOutput> DeleteAsync(long id)
        {
            return await _apiDomainService.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
       [HttpPost]
       public async Task<IResultOutput> BatchDeleteAsync(long[] ids)
        {
            return await _apiDomainService.BatchDeleteAsync(ids);
        }
    }
}
