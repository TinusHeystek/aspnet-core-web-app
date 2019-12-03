using Example.App.Shared.Interfaces.ApiControllers;
using Example.Shared.Core.Clients.ApiClients;

namespace Example.App.Shared.Interfaces.ApiClients
{
    public interface IContactClient : IApiClient, IContactController
    {
    }
}