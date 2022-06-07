using System;
using System.Collections.Generic;
using System.Text;

namespace AuCasbin.DomainService.Admin
{
   public interface ICasbinDomainService
    {
        void GetCheck(string userName, string domain, string permissionName);
    }
}
