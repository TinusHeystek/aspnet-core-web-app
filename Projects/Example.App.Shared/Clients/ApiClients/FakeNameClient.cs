using System.Threading;
using System.Threading.Tasks;
using Example.App.Shared.Interfaces.ApiClients;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.Queries;
using Example.Shared.Core.Clients.ApiClients;
using Flurl;
using Flurl.Http;

namespace Example.App.Shared.Clients.ApiClients
{
    public class FakeNameClient : ApiClient, IFakeNameClient
    {
        public FakeNameClient(string apiRouteBase) : base(apiRouteBase)
        {
            ApiRouteBase = ApiRouteBase.AppendPathSegment("FakeName");
        }

        public async Task<GetFakeNamesResult> GetFakeNames(int count, CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase
                .AppendPathSegments(nameof(GetFakeNames), count);

            return await url.GetJsonAsync<GetFakeNamesResult>(cancellationToken);
        }

        public async Task<GenerateContactResult> GenerateContacts(int count, CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase
                .AppendPathSegments(nameof(GenerateContacts), count);

            return await url.GetJsonAsync<GenerateContactResult>(cancellationToken);
        }
    }
}