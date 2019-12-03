using System.Threading;
using System.Threading.Tasks;
using Example.App.CQS.Queries;
using Example.App.Data.Context;
using Example.App.Shared.Models.Commands;
using Example.Shared.Core.Models;
using MediatR;

namespace Example.App.CQS.Commands
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IContactsDbContext _context;

        public DeleteContactCommandHandler(IMediator mediator, IContactsDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<CommandResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _mediator.Send(new GetContactByIdQuery(request.Id), cancellationToken);

            _context.Contacts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult().SuccessWithId(entity.Id);
        }
    }
}
