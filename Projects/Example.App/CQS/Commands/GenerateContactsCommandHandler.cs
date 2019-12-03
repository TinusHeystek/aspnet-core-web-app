using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Queries;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View.Contact;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Example.App.CQS.Commands
{
    public class GenerateContactsCommandHandler : IRequestHandler<GenerateContactsCommand, GenerateContactResult>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
       
        public GenerateContactsCommandHandler(ILogger<GenerateContactsCommandHandler> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<GenerateContactResult> Handle(GenerateContactsCommand request, CancellationToken cancellationToken)
        {
            var result =  await _mediator.Send(new GetFakeNamesQuery(request.Count), cancellationToken);
            _logger.LogDebug("Received {FakeNameCount} Fake Names", result.FakeNames.Count);
            var contacts = _mapper.Map<List<ContactModel>>(result.FakeNames);

            return new GenerateContactResult
            {
                Contacts = contacts,
                ContactCount = contacts.Count,
                PetCount = contacts.SelectMany(contact => contact.Pets).Count()
            };
        }
    }
}
