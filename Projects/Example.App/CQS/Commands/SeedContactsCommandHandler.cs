using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Data.Context;
using Example.App.Shared.Models.Commands;
using Example.Shared.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Guid = System.Guid;

namespace Example.App.CQS.Commands
{
    public class SeedContactsCommandHandler : IRequestHandler<SeedContactsCommand, SeedContactResult>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IContactsDbContext _context;
       
        public SeedContactsCommandHandler(ILogger<SeedContactsCommandHandler> logger, IMediator mediator, IContactsDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

        public async Task<SeedContactResult> Handle(SeedContactsCommand request, CancellationToken cancellationToken)
        {
            var result =  await _mediator.Send(new GenerateContactsCommand(request.Count), cancellationToken);
            _logger.LogDebug("Generated {ContactCount} Contacts with {PetCount} Pets", result.ContactCount, result.PetCount);

            var transactionId = Guid.Empty;
            try
            {
                transactionId = await _context.BeginTransactionAsync(cancellationToken);
                var tasks = new List<Task<CommandResult>>();
                result.Contacts.ForEach(contact => tasks.Add(_mediator.Send(new CreateContactCommand(contact), cancellationToken)));  
                var completedResults = await Task.WhenAll(tasks);
                await _context.CommitAsync(transactionId, cancellationToken);

                return new SeedContactResult
                {
                    IsSuccessful = completedResults.All(r => r.IsSuccessful),
                    SeedCount = completedResults.Count(r => r.IsSuccessful),
                    ExceptionMessages = completedResults.SelectMany(r => r.ExceptionMessages).ToList()
                };
            }
            catch (Exception)
            {
                await _context.RollbackAsync(transactionId, cancellationToken);
                throw;
            }
        }
    }
}
