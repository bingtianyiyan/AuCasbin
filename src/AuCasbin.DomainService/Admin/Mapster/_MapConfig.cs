
using AuCasbin.Domain;
using AuCasbin.TransData;
using AuCasbin.TransData.Api;
using Mapster;
using System.Linq;

namespace AuCasbin.DomainService
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class MapConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config
            //.NewConfig<UserEntity, UserGetOutput>()
            //.Map(dest => dest.RoleIds, src => src.Roles.Select(a => a.Id));

            //config
            //.NewConfig<AdUser, UserListOutput>()
            //.Map(dest => dest.RoleNames, src => src.Roles.Select(a => a.Name));
        }
    }
}