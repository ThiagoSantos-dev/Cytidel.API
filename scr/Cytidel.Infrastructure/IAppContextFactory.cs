using Cytidel.Application;

namespace Cytidel.Infrastructure;

internal interface IAppContextFactory
{
    IAppContext Create();
}
