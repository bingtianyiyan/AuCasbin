using AuCasbin.Core.Db;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.Repository.Admin.Implement
{
    public class OrganizationRepository : RepositoryBase<TOrganization>, IOrganizationRepository
    {
        public OrganizationRepository(DbUnitOfWorkManager uowm) : base(uowm)
        {
        }
    }
}
