using Library.LeoTumbas.Contracts.DTOs;
using Microsoft.AspNetCore.Authentication;

namespace Library.LeoTumbas.Services
{
    public interface IExtendedAuthenticationService : IAuthenticationService
    {
        Task<TokenDto> Login(LoginRequestDto loginRequest);
    }
}
