namespace Cytidel.Core.Exceptions;

public class InvalidAggregateIdException(Guid id) : DomainException($"Invalid aggregate id: {id}.")
{
    public Guid Id { get; } = id;
    public override string Code { get; } = "invalid_aggregate_id";
}
