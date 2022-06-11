using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.LeoTumbas.Contracts.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.LeoTumbas.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto GenerateToken(List<Claim> claims)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(keyBytes);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(int.Parse(_configuration["JWT:ValidHours"])),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new TokenDto
            {
                Token = token,
                ExpiresAt = jwtSecurityToken.ValidTo,
            };
        }
    }
}
