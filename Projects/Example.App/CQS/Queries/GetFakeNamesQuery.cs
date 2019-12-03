using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Shared.Models.Queries;
using Example.App.Shared.Models.View;
using Flurl.Http;
using MediatR;

namespace Example.App.CQS.Queries
{
    public class GetFakeNamesQuery : IRequest<GetFakeNamesResult>
    {
        public int Count { get; }

        public GetFakeNamesQuery(int count)
        {
            Count = count;
        }
    }

    

    public class GetFakeNamesQueryHandler : IRequestHandler<GetFakeNamesQuery, GetFakeNamesResult>
    {
        public const string FakeNameUrl = "https://api.namefake.com";
        
        public async Task<GetFakeNamesResult> Handle(GetFakeNamesQuery request, CancellationToken cancellationToken)
        {
            var fakeNames = await GenerateFakeNames(request.Count, cancellationToken);
            return new GetFakeNamesResult
            {
                FakeNames = fakeNames,
                FakeNamesCount = fakeNames.Count
            };
        }

        private async Task<List<FakeName>> GenerateFakeNames(int count, CancellationToken cancellationToken)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var tasks = new List<Task<FakeName>>();
            for (var x = 0; x < count; x++)
            {
                tasks.Add(FakeNameUrl.GetJsonAsync<FakeName>(cancellationToken)
                    .ContinueWith(task =>
                    {
                        if (!task.IsCompletedSuccessfully)
                            cts.Cancel(); 
                        return task.Result;
                    }, cts.Token));
            }
            var fakeNames = await Task.WhenAll(tasks);
            return fakeNames.ToList();
        }
    }
}
