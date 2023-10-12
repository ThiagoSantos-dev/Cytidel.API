namespace Cytidel.Core.Exceptions;

public class InvalidTitleException() : DomainException($"Invalid title.")
{
    public override string Code { get; } = "invalid_title";
}