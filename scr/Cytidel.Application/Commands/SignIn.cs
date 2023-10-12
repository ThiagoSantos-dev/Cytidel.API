using Omatka.CQRS.Commands;

namespace Cytidel.Application.Commands;

[Contract]
public class SignIn(string email, string password) : ICommand
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
