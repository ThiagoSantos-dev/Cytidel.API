namespace Cytidel.Application;
//application context interface
public interface IAppContext
{
    string RequestId { get; }
    IIdentityContext Identity { get; }
}
