using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Example.App.Controllers.ODataControllers.Interfaces
{
    public interface IODataController
    {
        Task<IActionResult> GetAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}