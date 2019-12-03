using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Shared.Interfaces.ApiClients;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View;
using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Clients.ApiClients;
using Example.Shared.Core.Models;
using Flurl;
using Flurl.Http;

namespace Example.App.Shared.Clients.ApiClients
{
    public class ContactClient: ApiClient, IContactClient
    {
        public ContactClient(string apiRouteBase) : base(apiRouteBase)
        {
            ApiRouteBase = ApiRouteBase.AppendPathSegment("Contact");
        }

        public async Task<PagedResult<ContactModel>> Get(int page, int pageSize, CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase
                .SetQueryParam(nameof(page), page)
                .SetQueryParam(nameof(pageSize), pageSize);
            return await url.GetJsonAsync<PagedResult<ContactModel>>(cancellationToken);
        }

        public async Task<PagedResult<ContactSummary>> GetSummary(int page, int pageSize, CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase
                .AppendPathSegment(nameof(GetSummary))
                .SetQueryParam(nameof(page), page)
                .SetQueryParam(nameof(pageSize), pageSize);
            return await url.GetJsonAsync<PagedResult<ContactSummary>>(cancellationToken);
        }

        public async Task<IEnumerable<ContactModel>> GetAll(CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase.AppendPathSegment(nameof(GetAll));
            return await url.GetJsonAsync<IEnumerable<ContactModel>>(cancellationToken);
        }

        public async Task<ContactModel> GetById(int id, CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase.AppendPathSegment(id);
            return await url.GetJsonAsync<ContactModel>(cancellationToken);
        }

        public async Task<long> Count(CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase.AppendPathSegment(nameof(Count));
            var response = await url.GetStringAsync(cancellationToken);
            if (long.TryParse(response, out var count))
                return count;
            return -1;
        }

        public async Task<CommandResult> CreateContact(ContactModel contactModel, CancellationToken cancellationToken = new CancellationToken())
        {
            return await ApiRouteBase.PostJsonAsync(contactModel, cancellationToken)
                .ReceiveJson<CommandResult>();
        }

        public async Task<CommandResult> UpdateContact(int id, ContactModel contactModel, CancellationToken cancellationToken = new CancellationToken())
        {
            return await ApiRouteBase
                .AppendPathSegment(id)
                .PutJsonAsync(contactModel, cancellationToken)
                .ReceiveJson<CommandResult>();
        }

        public async Task<CommandResult> DeleteContact(int id, CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase.AppendPathSegment(id);
            return await url.DeleteAsync(cancellationToken)
                .ReceiveJson<CommandResult>();
        }

        public async Task<SeedContactResult> GenerateContacts(int count, CancellationToken cancellationToken)
        {
            var url = ApiRouteBase.AppendPathSegments(nameof(GenerateContacts), count);
            return await url.PostJsonAsync(null, cancellationToken)
                .ReceiveJson<SeedContactResult>();
        }
    }
}
