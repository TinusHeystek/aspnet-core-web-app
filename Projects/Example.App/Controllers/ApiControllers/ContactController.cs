using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.App.CQS.Queries;
using Example.App.Shared.Interfaces.ApiControllers;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View;
using Example.App.Shared.Models.View.Contact;
using Example.Core.WebApi;
using Example.Shared.Core.Extensions;
using Example.Shared.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example.App.Controllers.ApiControllers
{
    public class ContactController : BaseApiController, IContactController
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedResult<ContactModel>> Get(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(new GetContactsModelQuery(), cancellationToken);
            return query.GetPaged(page, pageSize);
        }

        [HttpGet(nameof(GetSummary))]
        public async Task<PagedResult<ContactSummary>> GetSummary(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(new GetContactSummaryQuery(), cancellationToken);
            return query.GetPaged(page, pageSize);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IEnumerable<ContactModel>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetContactsModelQuery(), cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ContactModel> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetContactModelByIdQuery(id), cancellationToken);
            return result;
        }

        [HttpGet(nameof(Count))]
        public async Task<long> Count(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(new GetContactCountQuery(), cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<CommandResult> CreateContact([FromBody] ContactModel contact, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateContactCommand(contact), cancellationToken);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<CommandResult> UpdateContact(int id, [FromBody] ContactModel contact, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateContactCommand(contact), cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<CommandResult> DeleteContact(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteContactCommand(id), cancellationToken);
            return result;
        }

        [HttpPost(nameof(GenerateContacts) + "/{count}")]
        public async Task<SeedContactResult> GenerateContacts(int count, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SeedContactsCommand(count), cancellationToken);
            return result;
        }
    }
}
