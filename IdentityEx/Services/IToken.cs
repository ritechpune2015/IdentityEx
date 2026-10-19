using IdentityEx.Models;

namespace IdentityEx.Services
{
    public interface IToken
    {
        string GenerateToken(User rec);
    }
}
