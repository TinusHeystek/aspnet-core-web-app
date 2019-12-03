using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Clients.ODataClients;

namespace Example.App.Shared.Interfaces.ODataClients
{
    public interface IContactsODataClient: IODataClient<ContactModel>
    {
    }
}