using Cytidel.Application.Commands;
using Cytidel.Application.Events.RejectedEvents;
using Cytidel.Application.Exceptions;
using Cytidel.Core.Exceptions;
using Omatka.MessageBrokers.RabbitMQ;

namespace Cytidel.Infrastructure.Exceptions;

internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
{
    //Mapper Exceptions to return to the user.
    public object Map(Exception exception, object message)
        => exception switch

        {
            UserAlreadyExistsException ex => new SignUpRejected(ex.Email, ex.Message, ex.Code),
            InvalidCredentialsException ex => new SignInRejected(ex.Email, ex.Message, ex.Code),
            InvalidEmailException ex => message switch
            {
                SignIn command => new SignInRejected(command.Email, ex.Message, ex.Code),
                SignUpRejected command => new SignUpRejected(command.Email, ex.Message, ex.Code),
                _ => null
            },
            _ => null
        };
}
