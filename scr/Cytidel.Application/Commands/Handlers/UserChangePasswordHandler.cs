using Cytidel.Application.Exceptions;
using Cytidel.Application.Services;
using Cytidel.Core.Entities;
using Cytidel.Core.Repositories;
using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands.Handlers;

internal sealed class UserChangePasswordHandler(IUserRepository userRepository, 
    IPasswordService passwordService) : ICommandHandler<UserChangePassword>
{
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IUserRepository _userRepository = userRepository;
    public async Task HandleAsync(UserChangePassword command, CancellationToken cancellationToken = default)
    {
        //check if exists on the database.
        var user = await _userRepository.GetUserByEmailAsync(command.Email, cancellationToken)
         ?? throw new UserNotFoundException(command.Email);
        //hash the password
        var password = _passwordService.Hash(command.Password);
        //create a new object to update.
        var updatedUser = User.ChangePassword(user,password);
        //update the user on the database.
        await _userRepository.UpdateAsync(updatedUser);
    }
}
