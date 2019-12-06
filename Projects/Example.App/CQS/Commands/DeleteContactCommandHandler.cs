using System.Threading;
using System.Threading.Tasks;
using Example.App.Data.Context;
using Example.App.Data.Extensions;
using Example.App.Shared.Models.Commands;
using Example.Shared.Core.Models;
using MediatR;

namespace Example.App.CQS.Commands
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, CommandResult>
    {
        private readonly IContactsDbContext _context;

        public DeleteContactCommandHandler(IContactsDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contacts.FindOrThrowAsync(request.Id, cancellationToken);
            _context.Contacts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult().SuccessWithId(entity.Id);
        }
    }
}
