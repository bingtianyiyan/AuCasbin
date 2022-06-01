using System.Security.Claims;

namespace AuCasbin.Core.Auth
{
    public interface IUserToken
    {
        string Create(Claim[] claims);

        Claim[] Decode(string jwtToken);
    }
}