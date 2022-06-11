using System.Security.Claims;
using Library.LeoTumbas.Contracts.DTOs;

namespace Library.LeoTumbas.Services
{
    public interface ITokenGenerator
    {
        TokenDto GenerateToken(List<Claim> claims);
    }
}
