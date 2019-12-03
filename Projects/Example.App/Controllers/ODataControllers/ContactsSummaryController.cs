using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Controllers.ODataControllers.Interfaces;
using Example.App.CQS.Queries;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace Example.App.Controllers.ODataControllers
{
    public class ContactSummaryController : ODataController, IODataController
    {
        private readonly IMediator _mediator;

        public ContactSummaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // http://localhost:5000/odata/ContactSummary?$filter=eyeColor eq 'Blue' and initials eq 'M'&$count=true
        [EnableQuery(PageSize = 20)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = new CancellationToken())
        {
            var query = await _mediator.Send(new GetContactSummaryQuery(), cancellationToken);
            return Ok(query);
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] long key, CancellationToken cancellationToken = new CancellationToken())
        {
            var query = await _mediator.Send(new GetContactSummaryQuery(), cancellationToken);
            return Ok(query.FirstOrDefault(c => c.Id == key));
        }
    }
}
