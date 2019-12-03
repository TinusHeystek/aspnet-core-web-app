using System.Threading;
using System.Threading.Tasks;
using Example.Shared.Core.Clients.ApiClients;
using Example.Shared.Core.Models;
using Flurl;
using Flurl.Http;

namespace Example.Shared.Core.Clients.ODataClients
{
    public class ODataClient<T> : ApiClient, IODataClient<T>
    {
        public ODataClient(string apiRouteBase) : base(apiRouteBase)
        {
        }

        public async Task<PageResult<T>> Get(string select = null, string expand = null, string filter = null,
            string orderBy = null, int? top = null, int? skip = null, bool? count = null, string search = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var url = BuildODataUrl(ApiRouteBase, select, expand, filter, orderBy, top, skip, count, search);
            return await url.GetJsonAsync<PageResult<T>>(cancellationToken);
        }

        public async Task<T> GetByKey(int key, string select = null, string expand = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var url = ApiRouteBase.AppendPathSegment(key);
            url = BuildODataUrl(url, select, expand);
            return await url.GetJsonAsync<T>(cancellationToken);
        }

        public string BuildODataUrl(string url, string select = null, string expand = null, string filter = null, 
            string orderBy = null, int? top = null, int? skip = null, bool? count = null, string search = null)
        {
            if (!string.IsNullOrEmpty(select))
                url.SetQueryParam("$select", select);

            if (!string.IsNullOrEmpty(expand))
                url.SetQueryParam("$expand", expand);

            if (!string.IsNullOrEmpty(filter))
                url.SetQueryParam("$filter", filter);

            if (!string.IsNullOrEmpty(orderBy))
                url.SetQueryParam("$orderby", orderBy);

            if (top.HasValue && top.Value >= 0)
                url.SetQueryParam("$top", top.Value);

            if (skip.HasValue && skip.Value >= 0)
                url.SetQueryParam("$skip", skip.Value);

            if (count.HasValue && count.Value)
                url.SetQueryParam("$count", "true");

            if (!string.IsNullOrEmpty(search))
                url.SetQueryParam("$search", search);

            return url;
        }
    }
}