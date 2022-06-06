using AuCasbin.Core.Db;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.Repository.Admin.Implement
{
   public class CasbinRuleRepository : RepositoryBase<TCasbinRule>, ICasbinRuleRepository
    {
        public CasbinRuleRepository(DbUnitOfWorkManager uowm) : base(uowm)
        {
        }
    }
}
