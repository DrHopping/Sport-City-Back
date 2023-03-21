using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Interfaces;
using SportCity.Core.Services;

namespace SportCity.Web.Helpers;

public static class ServiceFactory
{
    public static IOwnableEntityService GetDomainServiceFor(this IServiceProvider provider, Type t)
    {
        return t.Name switch
        {
            nameof(Player) => provider.GetService<IPlayerService>(),
            _ => null
        } ?? throw new InvalidOperationException();
    }
}
