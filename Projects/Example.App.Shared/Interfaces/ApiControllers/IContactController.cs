using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View;
using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Models;

namespace Example.App.Shared.Interfaces.ApiControllers
{
    public interface IContactController
    {
        Task<PagedResult<ContactModel>> Get(int page, int pageSize, CancellationToken cancellationToken = new CancellationToken());
        Task<PagedResult<ContactSummary>> GetSummary(int page, int pageSize, CancellationToken cancellationToken = new CancellationToken());
        Task<IEnumerable<ContactModel>> GetAll(CancellationToken cancellationToken = new CancellationToken());
        Task<ContactModel> GetById(int id, CancellationToken cancellationToken = new CancellationToken());
        Task<long> Count(CancellationToken cancellationToken = new CancellationToken());
        Task<CommandResult> CreateContact(ContactModel contactModel, CancellationToken cancellationToken = new CancellationToken());
        Task<CommandResult> UpdateContact(int id, ContactModel contactModel, CancellationToken cancellationToken = new CancellationToken());
        Task<CommandResult> DeleteContact(int id, CancellationToken cancellationToken = new CancellationToken());
        Task<SeedContactResult> GenerateContacts(int count, CancellationToken cancellationToken = new CancellationToken());
    }
}