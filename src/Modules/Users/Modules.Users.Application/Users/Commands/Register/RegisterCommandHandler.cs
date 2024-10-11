using Modules.Users.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Common.Base;
using Common.Exceptions;
using Modules.Users.Application.Users.DTOs;
using FluentValidation;
using Modules.Users.Application.Common.Interfaces;

namespace Modules.Users.Application.Users.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<AuthResultDTO>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtToken _jwtToken;

    public RegisterCommandHandler(
        UserManager<AppUser> userManager,
        IJwtToken jwtToken
        )
    {
        _jwtToken = jwtToken;
        _userManager = userManager;
    }
    public async Task<ApiResponse<AuthResultDTO>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if user already exists:
        //
        var userExists = await _userManager.FindByEmailAsync(command.registerDTO.Email);
        if (userExists != null) throw new BadRequestException($"An account with the email : {command.registerDTO.Email} already exists");


        // Create User :
        var newUser = new AppUser
        {
            Email = command.registerDTO.Email,
            FullName = command.registerDTO.FullName,
            UserName = command.registerDTO.Email,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };

        var createdUser = await _userManager.CreateAsync(newUser, command.registerDTO.Password);

        if (!createdUser.Succeeded) throw new BadRequestException("An error occured registering your account");

        /*var addUserToRoleResult = await _userManager.AddToRoleAsync(newUser, "USER");*/

        /*if (!addUserToRoleResult.Succeeded) throw new BadRequestException("An error occured setting your role");*/
        var accessToken = await _jwtToken.GenerateToken(newUser);
        return new ApiResponse<AuthResultDTO>
        {
            IsSuccess = true,
            Message = "Registered successfully, Please check your email to confirm",
            StatusCode = 200,
            Response = new AuthResultDTO(accessToken)
        };

    }
}

public record RegisterCommand(
    RegisterRequestDTO registerDTO
) : IRequest<ApiResponse<AuthResultDTO>>;



public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.registerDTO.Email)
       .NotEmpty()
       .WithMessage("{PropertyName} is required")
       .NotNull()
       .EmailAddress()
       .OverridePropertyName("Email");

        RuleFor(x => x.registerDTO.Password)
        .NotEmpty()
        .WithMessage("Your password cannot be empty")
        .MinimumLength(8)
        .WithMessage("Your password length must be at least 8.")
        .MaximumLength(16)
        .WithMessage("Your password length must not exceed 16.")
        .Matches(@"[A-Z]+")
        .WithMessage("Your password must contain at least one uppercase letter.")
        .Matches(@"[a-z]+")
        .WithMessage("Your password must contain at least one lowercase letter.")
        .Matches(@"[0-9]+")
        .WithMessage("Your password must contain at least one number.")
        .Must(ContainNonAlphanumeric)
        .WithMessage("Your password must contain at least one non-alphanumeric character eg: (@ $ / !)")
        .OverridePropertyName("Password");

        RuleFor(x => x.registerDTO.FullName)
        .NotEmpty()
        .WithMessage("{PropertyName} is required")
        .NotNull()
        .OverridePropertyName("Fullname");
    }
    private bool ContainNonAlphanumeric(string password)
    {
        return password.Any(ch => !char.IsLetterOrDigit(ch));
    }
}

