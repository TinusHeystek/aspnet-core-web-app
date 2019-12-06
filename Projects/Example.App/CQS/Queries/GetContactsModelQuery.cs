using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.App.Data.Context;
using Example.App.Shared.Models.View.Contact;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.App.CQS.Queries
{
    public class GetContactsModelQuery : IRequest<IQueryable<ContactModel>>
    {
        
    }

    public class GetContactsModelQueryHandler : IRequestHandler<GetContactsModelQuery, IQueryable<ContactModel>>
    {
        private readonly IContactsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetContactsModelQueryHandler(IContactsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<IQueryable<ContactModel>> Handle(GetContactsModelQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.Contacts.AsNoTracking()
                    .ProjectTo<ContactModel>(_mapper.ConfigurationProvider));
        }
    }
}
