using AuCasbin.Domain;
using AuCasbin.Infrastructure.Api;
using AuCasbin.Repository.Admin;
using AuCasbin.TransData.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuCasbin.Infrastructure.Extensions;
using AuCasbin.Core.Attributes;
using Yitter.IdGenerator;

namespace AuCasbin.DomainService.Admin.Implement
{
    /// <summary>
    /// 接口服务业务逻辑层
    /// </summary>
    public class ApiDomainService:BaseService,IApiDomainService
    {
        private readonly IApiRepository _apiRepository;

        public ApiDomainService(IApiRepository moduleRepository)
        {
            _apiRepository = moduleRepository;
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetAsync(long id)
        {
            var entity = await _apiRepository.FindAsync(id);
            ApiGetOutput result = null;
            if (entity != null)
            {
                result = new ApiGetOutput()
                {
                     Id = entity.FId,
                     Title = entity.FTitle,
                     Path = entity.FPath,
                     ParentId = entity.FParentId,
                     HttpMethods = entity.FAction
                };
            }
            return ResultOutput.Ok(result);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetListAsync(string key)
        {
            var list = await _apiRepository
                .WhereIf(key.NotNull(), a => a.FPath.Contains(key) || a.FTitle.Contains(key))
                .ToListAsync();
            List<ApiListOutput> data = null;
            if (list != null && list.Count > 0){
                data = new List<ApiListOutput>(list.Count);
                list.ForEach(item =>
                {
                    data.Add(new ApiListOutput()
                    {
                         Id = item.FId,
                         ParentId = item.FParentId,
                         Path = item.FPath,
                         Title = item.FTitle,
                          HttpMethods = item.FAction
                    });
                });
            }
            return ResultOutput.Ok(data);
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetPageAsync(PageInput<ApiGetPageDto> input)
        {
            var key = input.Filter?.Title;

            var list = await _apiRepository.Select
            //.WhereDynamicFilter(input.DynamicFilter)
            .WhereIf(key.NotNull(), a => a.FPath.Contains(key) || a.FTitle.Contains(key))
            .Count(out var total)
            .OrderByDescending(true, c => c.FId)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();

            var data = new PageOutput<TApi>()
            {
                List = list,
                Total = total
            };

            return ResultOutput.Ok(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> AddAsync(ApiAddInput input)
        {
            var entity = new TApi()
            {
                 FId = YitIdHelper.NextId(),
                 FParentId =input.ParentId,
                 FPath = input.Path,
                 FTitle = input.Title,
                 FAction = input.HttpMethods,
                 FCreatedTime = DateTime.Now
            };
            var id = (await _apiRepository.InsertAsync(entity)).FId;

            return ResultOutput.Result(id > 0);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> UpdateAsync(ApiUpdateInput input)
        {
            if (!(input?.Id > 0))
            {
                return ResultOutput.NotOk();
            }

            var entity = await _apiRepository.GetAsync(input.Id);
            if (!(entity?.FId > 0))
            {
                return ResultOutput.NotOk("接口不存在！");
            }

            entity.FParentId = input.ParentId;
            entity.FPath= input.Path;
            entity.FTitle=input.Title;
           
            await _apiRepository.UpdateAsync(entity);
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultOutput> DeleteAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = (await _apiRepository.DeleteAsync(m => m.FId == id)) > 0;
            }

            return ResultOutput.Result(result);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultOutput> BatchDeleteAsync(long[] ids)
        {
            var result = await _apiRepository.DeleteAsync(x=> ids.Contains(x.FId)) >0;

            return ResultOutput.Result(result);
        }

    
    }
}
