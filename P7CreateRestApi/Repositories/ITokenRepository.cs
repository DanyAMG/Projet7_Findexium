using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
