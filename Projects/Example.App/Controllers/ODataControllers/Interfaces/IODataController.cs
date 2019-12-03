using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Example.App.Controllers.ODataControllers.Interfaces
{
    public interface IODataController
    {
        Task<IActionResult> Get(CancellationToken cancellationToken = new CancellationToken());
        Task<IActionResult> Get(long key, CancellationToken cancellationToken = new CancellationToken());
    }
}