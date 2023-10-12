namespace Cytidel.Core.Exceptions;

public class InvalidEmailException(string email) : DomainException($"Invalid email: {email}.")
{
    public override string Code { get; } = "invalid_email";
}
