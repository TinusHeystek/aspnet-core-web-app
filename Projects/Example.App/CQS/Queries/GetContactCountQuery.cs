using System.Threading;
using System.Threading.Tasks;
using Example.App.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.App.CQS.Queries
{
    public class GetContactCountQuery : IRequest<long>
    {
        
    }

    public class GetContactCountQueryHandler : IRequestHandler<GetContactCountQuery, long>
    {
        private readonly IContactsDbContext _dbContext;

        public GetContactCountQueryHandler(IContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<long> Handle(GetContactCountQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Contacts.CountAsync(cancellationToken);
        }
    }
}