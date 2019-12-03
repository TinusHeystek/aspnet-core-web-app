using Example.Shared.Core.Models;
using MediatR;

namespace Example.App.Shared.Models.Commands
{
    public class DeleteContactCommand: IRequest<CommandResult>
    {
        public int Id { get; }

        public DeleteContactCommand(int id)
        {
            Id = id;
        }
    }
}
