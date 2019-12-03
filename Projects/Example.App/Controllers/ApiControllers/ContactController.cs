using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Queries;
using Example.App.Data.Models;
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
        private readonly IMapper _mapper;

        public ContactController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<PagedResult<ContactModel>> Get(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(new GetContactsQuery(), cancellationToken);
            var result = query.GetPaged<Contact, ContactModel>(_mapper, page, pageSize);
            return result;
        }

        [HttpGet(nameof(GetSummary))]
        public async Task<PagedResult<ContactSummary>> GetSummary(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(new GetContactSummaryQuery(), cancellationToken);
            var result = query.GetPaged(page, pageSize);
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IEnumerable<ContactModel>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetContactsQuery(), cancellationToken);
            var models = _mapper.Map<List<ContactModel>>(await result.ToListAsync(cancellationToken));
            return models;
        }

        [HttpGet("{id}")]
        public async Task<ContactModel> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetContactByIdQuery(id), cancellationToken);
            var model = _mapper.Map<ContactModel>(result);
            return model;
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
