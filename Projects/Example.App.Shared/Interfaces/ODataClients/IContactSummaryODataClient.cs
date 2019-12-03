using Example.App.Shared.Models.View;
using Example.Shared.Core.Clients.ODataClients;

namespace Example.App.Shared.Interfaces.ODataClients
{
    public interface IContactSummaryODataClient: IODataClient<ContactSummary>
    {
    }
}