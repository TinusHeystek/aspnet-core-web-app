using System.Threading;
using System.Threading.Tasks;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.Queries;

namespace Example.App.Shared.Interfaces.ApiControllers
{
    public interface IFakeNameController
    {
        Task<GetFakeNamesResult> GetFakeNames(int count, CancellationToken cancellationToken = new CancellationToken());
        Task<GenerateContactResult> GenerateContacts(int count, CancellationToken cancellationToken = new CancellationToken());
    }
}