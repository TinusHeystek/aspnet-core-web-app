using System.Threading;
using System.Threading.Tasks;
using Example.App.CQS.Queries;
using Example.App.Shared.Interfaces.ApiControllers;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.Queries;
using Example.Core.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.App.Controllers.ApiControllers
{
    public class FakeNameController : BaseApiController, IFakeNameController
    {
        private readonly IMediator _mediator;

        public FakeNameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetFakeNames/{count}")]
        public async Task<GetFakeNamesResult> GetFakeNames(int count, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFakeNamesQuery(count), cancellationToken);
            return result;
        }

        [HttpGet("GenerateContacts/{count}")]
        public async Task<GenerateContactResult> GenerateContacts(int count, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GenerateContactsCommand(count), cancellationToken);
            return result;
        }
    }
}