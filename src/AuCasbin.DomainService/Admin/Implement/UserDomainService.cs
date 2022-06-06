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
using AuCasbin.Infrastructure.Extensions;

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
            var info = await _userRepository.GetAsync<TUser>(x=>x.FUserName == "admin");
            Console.WriteLine(info?.FAvatar);

            var sqlList = _userRepository.Select.WithSql("select a.* from ad_user a inner join ad_user_role b on a.Id=b.UserId").ToList<TUser>();

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
            .OrderByDescending(true, a => a.FId)
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
            var models = new List<TUser>() {
                new TUser()
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
                await _userRepository.DeleteAsync(x => ids.Contains(x.FId));

            var result = await _userRepository.Where(x=> ids.Contains(x.FId)).ToDelete()
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

        /// <summary>
        /// 刷新Token
        /// 以旧换新
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        public async Task<IResultOutput> Refresh(string token)
        {
            var userClaims = LazyGetRequiredService<IUserToken>().Decode(token);
            if (userClaims == null || userClaims.Length == 0)
            {
                return ResultOutput.NotOk();
            }

            var refreshExpires = userClaims.FirstOrDefault(a => a.Type == ClaimAttributes.RefreshExpires)?.Value;
            if (refreshExpires.IsNull())
            {
                return ResultOutput.NotOk();
            }

            if (refreshExpires.ToLong() <= DateTime.Now.ToTimestamp())
            {
                return ResultOutput.NotOk("登录信息已过期");
            }

            var userId = userClaims.FirstOrDefault(a => a.Type == ClaimAttributes.UserId)?.Value;
            if (userId.IsNull())
            {
                return ResultOutput.NotOk("登录信息已失效");
            }
            //获取用户信息
            //var output = await LazyGetRequiredService<IUserService>().GetLoginUserAsync(userId.ToLong());
            string newToken = GetTokenTest();//GetToken(output?.Data);
            return ResultOutput.Ok(new { token = newToken });
        }
        #endregion


    }
}
