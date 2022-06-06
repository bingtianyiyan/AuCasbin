using AuCasbin.Core.Db;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.Repository.Admin.Implement
{
   public class MenuRepository : RepositoryBase<TMenu>, IMenuRepository
    {
        public MenuRepository(DbUnitOfWorkManager uowm) : base(uowm)
        {
        }
    }
}
