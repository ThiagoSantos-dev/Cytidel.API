using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands;

[Contract]
public class UserChangePassword(string email, string password) : ICommand
{
    public string Email { get; } = email;
    public string Password { get; } = password;
}
