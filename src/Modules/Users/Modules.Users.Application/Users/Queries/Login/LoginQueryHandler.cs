using Modules.Users.Domain.Entities;
using MediatR;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Modules.Users.Application.Users.DTOs;
using Modules.Users.Application.Common.Interfaces;
using Common.Base;

namespace Modules.Users.Application.Users.Queries.Login
{

    public class LoginQueryHandler : IRequestHandler<LoginQuery, ApiResponse<AuthResultDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtToken _jwtToken;
        public LoginQueryHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            IJwtToken jwtToken
            )
        {
            _jwtToken = jwtToken;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task<ApiResponse<AuthResultDTO>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {

            // 1. Validate if User does Exist
            var user = await _userManager.FindByEmailAsync(query.loginDTO.Email);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), query.loginDTO.Email);
            }


            //// 2. Validate if Password Is Correct
            var result = await _signinManager.CheckPasswordSignInAsync(user, query.loginDTO.Password, false);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Credentials Does Not Match");
            }
            //// 3. Create JWT Token and Assign Roles


            var accessToken = await _jwtToken.GenerateToken(user);




            return new ApiResponse<AuthResultDTO>
            {
                IsSuccess = true,
                Message = "You are logged in successfully",
                StatusCode = 200,
                Response = new AuthResultDTO(accessToken)
            };
        }
    }
    public record LoginQuery(
        LoginRequestDTO loginDTO
    ) : IRequest<ApiResponse<AuthResultDTO>>;
}
