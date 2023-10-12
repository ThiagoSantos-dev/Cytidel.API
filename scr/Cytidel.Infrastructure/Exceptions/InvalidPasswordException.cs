namespace Cytidel.Infrastructure.Exceptions;

internal class InvalidPasswordException(string email) : InfraException($"Invalid password on account email: {email}.")
{
     public override string Code { get; } = "invalid_credentials";
    public string Email { get; } = email;
}
