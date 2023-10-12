using Omatka.CQRS.Events;

namespace Cytidel.Application.Events.RejectedEvents
{
    //notify user if the request not been fullfil
    [Contract]
    public class SignInRejected(string email, string reason, string code) : IRejectedEvent
    {
        public string Email { get; } = email;
        public string Reason { get; } = reason;
        public string Code { get; } = code;
    }
}
