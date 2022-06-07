using AuCasbin.Core.Repositories;
using AuCasbin.Domain;
using NetCasbin.Persist;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.Repository.Admin
{
   public partial interface ICasbinRuleRepository : IRepositoryBase<TCasbinRule>, IAdapter
    {
    }
}
