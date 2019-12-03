using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Data.Context;
using Example.App.Data.Models;
using MediatR;

namespace Example.App.CQS.Queries
{
    public class GetContactByIdQuery : IRequest<Contact>
    {
        public int Id { get; }
        public GetContactByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact>
    {
        private readonly IContactsDbContext _dbDbContext;

        public GetContactByIdQueryHandler(IContactsDbContext dbDbContext)
        {
            _dbDbContext = dbDbContext;
        }

        public async Task<Contact> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbDbContext.Contacts.FindAsync(request.Id);
            if (entity == null)
                throw new KeyNotFoundException($"Contact with Id[{request.Id}] not found.");
            return entity;
        }
    }
}
