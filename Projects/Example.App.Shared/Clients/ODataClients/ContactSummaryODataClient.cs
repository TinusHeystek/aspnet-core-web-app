using Example.App.Shared.Interfaces.ODataClients;
using Example.App.Shared.Models.View;
using Example.Shared.Core.Clients.ODataClients;
using Flurl;

namespace Example.App.Shared.Clients.ODataClients
{
    public class ContactSummaryODataClient : ODataClient<ContactSummary>, IContactSummaryODataClient
    {
        public ContactSummaryODataClient(string apiRouteBase) : base(apiRouteBase)
        {
            ApiRouteBase = ApiRouteBase.AppendPathSegments("odata", "ContactSummary");
        }
    }
}