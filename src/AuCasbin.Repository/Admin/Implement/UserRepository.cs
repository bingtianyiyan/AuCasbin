using System;
using System.Collections.Generic;
using System.Text;
using AuCasbin.Core.Repositories;
using AuCasbin.Core.Db;
using AuCasbin.Domain;

namespace AuCasbin.Repository.Admin.Implement
{
    public class UserRepository : RepositoryBase<AdUser>, IUserRepository
    {
        public UserRepository(DbUnitOfWorkManager muowm) : base(muowm)
        {
        }
    }
}
