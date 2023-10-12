using Cytidel.Application.Exceptions;
using Cytidel.Core.Repositories;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class DeleteUserHandler(IUserRepository userRepository) : ICommandHandler<DeleteUser>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task HandleAsync(DeleteUser command, CancellationToken cancellationToken = default)
    {
        //check if exists on the database
        var user = await _userRepository.GetUserByIdAsync(command.Id, cancellationToken)
            ?? throw new UserNotFoundException(command.Id);

        //delete user from the database
        await _userRepository.DeleteAsync(command.Id);
    }
}
