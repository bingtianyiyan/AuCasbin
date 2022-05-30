using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuCasbin.DomainService.Admin
{
   public interface IUserDomainService
    {
        Task GetUserInfoAsync();
    }
}
