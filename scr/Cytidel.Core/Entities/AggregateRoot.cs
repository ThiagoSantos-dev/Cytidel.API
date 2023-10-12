namespace Cytidel.Core.Entities;
//use the aggregate root to share the necessary clams to add and control on MongoDb.
public abstract class AggregateRoot
{
    public AggregateId Id { get; protected set; }
    public int Version { get; protected set; }
    protected void AddEvent()
    {
        //update version
        Version++;
    }
}
