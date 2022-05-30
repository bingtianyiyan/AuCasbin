using System;
using System.Collections.Generic;
using System.Text;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;

namespace AuCasbin.Repository.Admin
{
    public interface IUserRepository : IRepositoryBase<AdUser>
    {
    }
}
