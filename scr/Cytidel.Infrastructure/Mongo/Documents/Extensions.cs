using Cytidel.Application.Dtos;
using Cytidel.Core.Entities;

namespace Cytidel.Infrastructure.Mongo.Documents;

//Mapper Entity to Document
public static class Extensions
{
    //Mapper Document to Entity
    public static User AsEntity(this UserDocument document)
        => new(document.Id, document.Email, document.FirstName, document.LastName, 
            document.Password, document.Username, document.Version);
    //Mapper Document to Entity
    public static ToDoTask AsEntity(this ToDoTaskDocument document)
        => new(document.Id, document.CreatedAt.AsDateTime(), document.Description,
            document.DueDate.AsDateTime(), document.Priority, document.Status, 
            document.Title, document.Version);
    //Mapper Entity to Document
    public static UserDocument AsDocument(this User entity)
        => new()
        {
            Id = entity.Id,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Password = entity.Password,
            Username = entity.Username,
            Version = entity.Version
        };
    //Mapper Entity to Document
    public static ToDoTaskDocument AsDocument(this ToDoTask entity)
        => new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt.AsDaysSinceEpoch(),
            Description = entity.Description,
            DueDate = entity.DueDate.AsDaysSinceEpoch(),
            Priority = entity.Priority,
            Status = entity.Status,
            Title = entity.Title,
            Version = entity.Version
        };
    //Mapper Document to Dto
    public static TaskDto AsDto(this ToDoTaskDocument document)
        => new()
        {
            Id = document.Id,
            Description = document.Description,
            DueDate = document.DueDate.AsDateTime(),
            Status = document.Status,
            Title = document.Title,
            Priority = document.Priority
        };
    //convert Datetime to long
    public static long AsDaysSinceEpoch(this DateTime dateTime)
        => (long)(dateTime - new DateTime()).TotalSeconds;
    //convert long to Datetime
    public static DateTime AsDateTime(this long daysSinceEpoch)
        => new DateTime().AddSeconds(daysSinceEpoch);
}
