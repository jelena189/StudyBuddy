using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudyBuddy.Domain.ServiceInterfaces;
using StudyBuddy.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StudyBuddy.Services.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly JwtSettings settings;
        public SecurityService(IOptions<JwtSettings> settings)
        {
            this.settings = settings.Value;
        }
        public string GenerateJwtToken(Guid userId, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, settings.Audience),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(settings.ExpirationInMinutes)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

}
