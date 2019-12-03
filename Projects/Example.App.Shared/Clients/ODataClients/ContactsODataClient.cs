using Example.App.Shared.Interfaces.ODataClients;
using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Clients.ODataClients;
using Flurl;

namespace Example.App.Shared.Clients.ODataClients
{
    public class ContactsODataClient : ODataClient<ContactModel>, IContactsODataClient
    {
        public ContactsODataClient(string apiRouteBase) : base(apiRouteBase)
        {
            ApiRouteBase = ApiRouteBase.AppendPathSegments("odata", "Contacts");
        }
    }
}