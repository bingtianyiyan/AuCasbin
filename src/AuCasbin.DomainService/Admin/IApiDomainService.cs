using AuCasbin.Infrastructure.Api;
using AuCasbin.TransData.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuCasbin.DomainService.Admin
{
   public interface IApiDomainService
    {
        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> GetAsync(long id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<IResultOutput> GetListAsync(string key);

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> GetPageAsync(PageInput<ApiGetPageDto> input);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> AddAsync(ApiAddInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> UpdateAsync(ApiUpdateInput input);

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> DeleteAsync(long id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultOutput> BatchDeleteAsync(long[] ids);
    }
}
