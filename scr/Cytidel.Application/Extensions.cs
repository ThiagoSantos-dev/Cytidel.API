using Omatka;
using Omatka.CQRS.Commands;
using Omatka.CQRS.Events;

namespace Cytidel.Application;

public static class Extensions
{
    //Registering the services 
    public static IOmatkaBuilder AddApplication(this IOmatkaBuilder builder)
        => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher();
}
