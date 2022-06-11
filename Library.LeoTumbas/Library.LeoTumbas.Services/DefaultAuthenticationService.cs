using System.Security.Claims;
using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Library.LeoTumbas.Services
{
    public class DefaultAuthenticationService : IExtendedAuthenticationService
    {
        private readonly UserManager<Person> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public DefaultAuthenticationService(UserManager<Person> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenDto> Login(LoginRequestDto loginRequest)
        {
            Person? user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                throw new UserDoesNotExistException(loginRequest.Email);
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (isPasswordValid is false)
            {
                throw new InvalidPasswordException(loginRequest.Password);
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Full name", user.FullName),
                new Claim("Id", user.Id.ToString()),
            };
            return _tokenGenerator.GenerateToken(claims);
        }

        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
