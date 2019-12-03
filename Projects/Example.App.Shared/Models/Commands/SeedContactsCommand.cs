using System.Collections.Generic;
using MediatR;

namespace Example.App.Shared.Models.Commands
{
    public class SeedContactsCommand: IRequest<SeedContactResult>
    {
        public int Count { get; }

        public SeedContactsCommand(int count)
        {
            Count = count;
        }
    }

    public class SeedContactResult
    {
        public bool IsSuccessful { get; set; }
        public int SeedCount { get; set; }
        public List<string> ExceptionMessages { get; set; }
    }

}
