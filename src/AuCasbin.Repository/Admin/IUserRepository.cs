using System;
using System.Collections.Generic;
using System.Text;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;

namespace AuCasbin.Repository.Admin
{
    public partial interface IUserRepository : IRepositoryBase<TUser>
    {
    }
}
