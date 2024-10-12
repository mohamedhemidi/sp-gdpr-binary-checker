using System.Security.Claims;
using System.Text;
using Modules.Users.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modules.Users.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Modules.Users.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Modules.Users.Application.Common.Services
{
    public class JwtToken : IJwtToken
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUser> _userManager;

        public JwtToken(IOptions<JwtSettings> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
        }
        public async Task<TokenType> GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName!),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: creds
            );

            return new TokenType
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                ExpiryDate = tokenDescriptor.ValidTo
            };
        }


        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                NameClaimType = "name",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, TokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }
    }
}
