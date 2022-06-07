using AuCasbin.Repository.Admin;
using NetCasbin;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.DomainService.Admin.Implement
{
   public class CasbinDomainService : BaseService, ICasbinDomainService
    {
        private readonly ICasbinRuleRepository _casbinRuleRepository;

        private readonly Enforcer _enforcer;

        public CasbinDomainService(ICasbinRuleRepository casbinRuleRepository)
        {
            _casbinRuleRepository = casbinRuleRepository;
            _enforcer = new Enforcer("CasbinConfig/rbac_model.conf", casbinRuleRepository);
        }

        public void GetCheck(string userName, string domain, string permissionName)
        {
            var response = _enforcer.Enforce(userName, domain, permissionName);

            var tt = _enforcer.AddPermissionForUser(userName, domain, permissionName);
            var response2 = _enforcer.Enforce(userName, domain, permissionName);
        }
    }
}
