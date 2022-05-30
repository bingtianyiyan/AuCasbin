using FreeSql;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuCasbin.Core.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 获得Dto
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<TDto> GetAsync<TDto>(TKey id);

        /// <summary>
        /// 根据条件获取Dto
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 递归删除
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="disableGlobalFilterNames">禁用全局过滤器名</param>
        /// <returns></returns>
        Task<bool> DeleteRecursiveAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames);

     
    }

    public interface IRepositoryBase<TEntity> : IRepositoryBase<TEntity, long> where TEntity : class
    {
    }
}