using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.App.Data.Context;
using Example.App.Shared.Models.View;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.App.CQS.Queries
{
    public class GetContactSummaryQuery : IRequest<IQueryable<ContactSummary>>
    {
        
    }

    public class GetContactsSummaryQueryHandler : IRequestHandler<GetContactSummaryQuery, IQueryable<ContactSummary>>
    {
        private readonly IContactsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetContactsSummaryQueryHandler(IContactsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<IQueryable<ContactSummary>> Handle(GetContactSummaryQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.Contacts.AsNoTracking().ProjectTo<ContactSummary>(_mapper.ConfigurationProvider));
        }
    }
}
