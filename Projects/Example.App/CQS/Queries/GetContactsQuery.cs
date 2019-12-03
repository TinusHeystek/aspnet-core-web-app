using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Data.Context;
using Example.App.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.App.CQS.Queries
{
    public class GetContactsQuery : IRequest<IQueryable<Contact>>
    {
        
    }

    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IQueryable<Contact>>
    {
        private readonly IContactsDbContext _dbContext;

        public GetContactsQueryHandler(IContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IQueryable<Contact>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.Contacts.AsNoTracking());
        }
    }

    
}
