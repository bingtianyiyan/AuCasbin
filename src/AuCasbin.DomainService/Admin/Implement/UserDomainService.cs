using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuCasbin.Repository.Admin;
using AuCasbin.Domain;

namespace AuCasbin.DomainService.Admin.Implement
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task GetUserInfoAsync()
        {
            var info = await _userRepository.GetAsync<AdUser>(x=>x.Id == 1);
            Console.WriteLine(info.Avatar);
           
        }


    }
}
