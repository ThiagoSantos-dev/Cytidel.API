using Cytidel.Application.Exceptions;
using Cytidel.Core.Entities;
using Cytidel.Core.Repositories;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class UpdateUserHandler(IUserRepository userRepository) : ICommandHandler<UpdateUser>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task HandleAsync(UpdateUser command, CancellationToken cancellationToken = default)
    {
        //check if exists on the database.
        var user = await _userRepository.GetUserByEmailAsync(command.Email, cancellationToken)
            ?? throw new UserNotFoundException(command.Email);
        //create a new object to update.
        var updatedUser = User.Update(user, command.FirstName, 
            command.LastName, command.Username);
        //update the user on the database.
        await _userRepository.UpdateAsync(updatedUser);
    }
}
