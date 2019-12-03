using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Validators;
using Example.App.Data.Context;
using Example.App.Data.Models;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Models;
using FluentValidation;
using MediatR;

namespace Example.App.CQS.Commands
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, CommandResult>
    {
        private readonly IContactsDbContext _context;
        private readonly IValidator<ContactModel> _contactValidator;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IContactsDbContext context, IValidator<ContactModel> contactValidator, IMapper mapper)
        {
            _context = context;
            _contactValidator = contactValidator;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            await _contactValidator.ValidateAndThrowAsync(request.ContactModel, ContactValidator.CreateRuleSet, cancellationToken);

            var entity = _mapper.Map<Contact>(request.ContactModel);
            await _context.Contacts.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult().SuccessWithId(entity.Id);
        }
    }
}
