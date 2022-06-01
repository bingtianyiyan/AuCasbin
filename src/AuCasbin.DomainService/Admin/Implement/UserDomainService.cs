using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuCasbin.Repository.Admin;
using AuCasbin.Domain;
using AuCasbin.Infrastructure.Api;
using AuCasbin.TransData;
using MapsterMapper;
using AuCasbin.Core.Attributes;
using AuCasbin.Core.Db;
using FreeSql;
using AuCasbin.Core.Auth;

namespace AuCasbin.DomainService.Admin.Implement
{
    /// <summary>
    /// 案例
    /// </summary>
    public class UserDomainService : BaseService,IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly DbUnitOfWorkManager _unitOfWorkManager;//可用于事务

        public UserDomainService(
            IUserRepository userRepository,
            DbUnitOfWorkManager unitOfWorkManager
        )
        {
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task GetUserInfoAsync()
        {
            var info = await _userRepository.GetAsync<AdUser>(x=>x.UserName == "admin");
            Console.WriteLine(info?.Avatar);

            var sqlList = _userRepository.Select.WithSql("select a.* from ad_user a inner join ad_user_role b on a.Id=b.UserId").ToList<AdUser>();

            await GetUserListAsync();

            var pageList = await GetPageAsync(new PageInput() {  CurrentPage=1,PageSize=10});

           await BatchDeleteAsync(new long[] { 1, 2 });

            GetTokenTest();
        }

        public async Task GetUserListAsync()
        {
            var list = await _userRepository.Select.ToListAsync();
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetPageAsync(PageInput input)
        {
            var list = await _userRepository.Select
          //  .WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total)
            .OrderByDescending(true, a => a.Id)
          //  .IncludeMany(a => a.Roles.Select(b => new RoleEntity { Name = b.Name }))
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();

            var data = new PageOutput<UserListOutput>()
            {
                List = Mapper.Map<List<UserListOutput>>(list),
                Total = total
            };

            return ResultOutput.Ok(data);
        }


        public async Task BatchInsertAsync()
        {
            var models = new List<AdUser>() {
                new AdUser()
                {

                }
            };
            await _userRepository.InsertAsync(models);
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Transaction]
        public async Task<IResultOutput> BatchDeleteAsync(long[] ids)
        {
            //事务
            using (IUnitOfWork unitOfWork = _unitOfWorkManager.Begin())
            {

            }
                await _userRepository.DeleteAsync(x => ids.Contains(x.Id));

            var result = await _userRepository.Where(x=> ids.Contains(x.Id)).ToDelete()
                .ExecuteAffrowsAsync();
            //await _userRoleRepository.DeleteAsync(a => ids.Contains(a.UserId));

            return ResultOutput.Result(result > 0);
        }

        #region Jwt Auth
        /// <summary>
        /// 获得token
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        private string GetTokenTest()
        {
            var token = LazyGetRequiredService<IUserToken>().Create(new[]
            {
                new Claim(ClaimAttributes.UserId, "1"),
                new Claim(ClaimAttributes.UserName, "test"),
                new Claim(ClaimAttributes.UserNickName, "test")
            });

            return token;
        }
        #endregion


    }
}
