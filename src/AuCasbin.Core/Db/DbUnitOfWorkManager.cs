using FreeSql;
using System;

namespace AuCasbin.Core.Db
{
    public class DbUnitOfWorkManager : UnitOfWorkManager
    {
        public DbUnitOfWorkManager(IdleBus<IFreeSql> ib, IServiceProvider serviceProvider) : base(ib.GetFreeSql(serviceProvider))
        {
        }
    }
}