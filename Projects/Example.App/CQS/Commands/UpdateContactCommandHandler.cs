using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Queries;
using Example.App.CQS.Validators;
using Example.App.Data.Context;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Example.App.CQS.Commands
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IContactsDbContext _context;
        private readonly IValidator<ContactModel> _contactValidator;

        public UpdateContactCommandHandler(IMediator mediator, IMapper mapper,
            IContactsDbContext context, IValidator<ContactModel> contactValidator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _contactValidator = contactValidator;
        }

        public async Task<CommandResult> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            await _contactValidator.ValidateAndThrowAsync(request.ContactModel, ContactValidator.UpdateRuleSet, cancellationToken);
            var entity = await _mediator.Send(new GetContactByIdQuery(request.ContactModel.Id), cancellationToken);
            _mapper.Map(request.ContactModel, entity);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult().SuccessWithId(entity.Id);
        }
    }
}
