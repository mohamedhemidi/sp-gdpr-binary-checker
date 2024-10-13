using System.Security.Claims;
using Common.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities;
using Common.Exceptions;
using Common.Contracts.Messaging.Events;
using MongoDB.Driver;
using MassTransit;
namespace Modules.Users.Application.Users.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ApiResponse<bool>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRequestClient<DeleteAllEntriesEvent> _client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMongoClient _mongoClient;
    public DeleteAccountCommandHandler(UserManager<AppUser> userManager,
                                       IHttpContextAccessor httpContextAccessor,
                                       IMongoClient mongoClient,
                                       IRequestClient<DeleteAllEntriesEvent> client)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mongoClient = mongoClient;
        _client = client;
    }


    public async Task<ApiResponse<bool>> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {

        var currentUser = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var user = await _userManager.FindByIdAsync(currentUser);

        if (user == null)
        {
            throw new NotFoundException(nameof(user), currentUser); ;
        }
        using (var session = await _mongoClient.StartSessionAsync())
        {
            session.StartTransaction();

            try
            {
                // Ensure removal of all entries related to user in the other module
                var entriesDeleted = await _client.GetResponse<CommandSuccess>(
                       new DeleteAllEntriesEvent(currentUser), cancellationToken
                );
                if (entriesDeleted.Message.Success)
                {
                    // If All related entries deleted succesfully proceed to delete the user's account
                    var deletion = await _userManager.DeleteAsync(user);

                    if (!deletion.Succeeded)
                    {
                        await session.AbortTransactionAsync();
                        throw new BadRequestException("An error occured deleting your account 11");
                    }

                    await session.CommitTransactionAsync();

                    return new ApiResponse<bool>
                    {
                        IsSuccess = true,
                        Message = "You account has been deleted succsfully",
                        StatusCode = 200,
                        Response = true,
                        StackTrace = null,
                    };
                }
                else
                {
                    await session.AbortTransactionAsync();
                    throw new BadRequestException("An error occured deleting your account 22");

                }

            }
            catch (System.Exception)
            {

                throw;
            }

        }




    }
}

public record DeleteAccountCommand(
) : IRequest<ApiResponse<bool>>;



