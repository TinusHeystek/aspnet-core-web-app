using System.Threading;
using System.Threading.Tasks;
using Example.Shared.Core.Clients.ApiClients;
using Example.Shared.Core.Models;

namespace Example.Shared.Core.Clients.ODataClients
{
    public interface IODataClient<T> : IApiClient
    {
        Task<PageResult<T>> Get(string select = null, string expand = null, string filter = null,
            string orderBy = null, int? top = null, int? skip = null, bool? count = null, string search = null,
            CancellationToken cancellationToken = new CancellationToken());

        Task<T> GetById(int id, string select = null, string expand = null,
            CancellationToken cancellationToken = new CancellationToken());

        string BuildODataUrl(string url, string select = null, string expand = null, string filter = null, 
            string orderBy = null, int? top = null, int? skip = null, bool? count = null, string search = null);
    }
}