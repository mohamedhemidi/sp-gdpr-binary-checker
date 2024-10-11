
using Modules.Users.Domain.Models;

namespace Modules.Users.Application.Users.DTOs;
public record AuthResultDTO(
    TokenType? AccessToken = null
);
