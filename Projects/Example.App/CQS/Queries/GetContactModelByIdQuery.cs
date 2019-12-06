using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.Data.Context;
using Example.App.Data.Extensions;
using Example.App.Data.Models;
using Example.App.Shared.Models.View.Contact;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.App.CQS.Queries
{
    public class GetContactModelByIdQuery : IRequest<ContactModel>
    {
        public int Id { get; }
        public GetContactModelByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetContactModelByIdQueryHandler : IRequestHandler<GetContactModelByIdQuery, ContactModel>
    {
        private readonly IContactsDbContext _dbDbContext;
        private readonly IMapper _mapper;

        public GetContactModelByIdQueryHandler(IContactsDbContext dbDbContext, IMapper mapper)
        {
            _dbDbContext = dbDbContext;
            _mapper = mapper;
        }

        public async Task<ContactModel> Handle(GetContactModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbDbContext.Contacts.AsNoTracking()
                .FindOrThrowAsync<Contact, ContactModel>(_mapper, request.Id, cancellationToken);
        }
    }
}
