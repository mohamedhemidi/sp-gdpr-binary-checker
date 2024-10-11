using Modules.Users.Application.Users.DTOs;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Models;

namespace Modules.Users.Application.Common.Interfaces
{
    public interface IJwtToken
    {
        Task<TokenType> GenerateToken(AppUser user);
    }
}
