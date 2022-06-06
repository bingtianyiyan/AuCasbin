using AuCasbin.Core.Db;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.Repository.Admin.Implement
{
    public class RoleRepository : RepositoryBase<TRole>, IRoleRepository
    {
        public RoleRepository(DbUnitOfWorkManager uowm) : base(uowm)
        {
        }
    }
}
