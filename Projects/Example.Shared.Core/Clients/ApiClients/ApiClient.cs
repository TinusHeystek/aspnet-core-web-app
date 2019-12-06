namespace Example.Shared.Core.Clients.ApiClients
{
    public class ApiClient : IApiClient
    {
        public string ApiRouteBase { get; set; }

        public ApiClient(string apiRouteBase)
        {
            ApiRouteBase = apiRouteBase;
        }
    }
}