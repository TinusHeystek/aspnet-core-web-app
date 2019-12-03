using System.Collections.Generic;

namespace Example.Shared.Core.Models
{
    public interface ICommandResult
    {
        int Id { get; set; }
        bool IsSuccessful { get; set; }
        List<string> ExceptionMessages { get; set; }
    }

    public class CommandResult : ICommandResult
    {
        public int Id { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> ExceptionMessages { get; set; }

        public CommandResult()
        {
            Id = 0;
            IsSuccessful = false;
            ExceptionMessages = new List<string>();
        }
    }

    public static class CommandResultExtensions
    {
        public static T SuccessWithId<T>(this T commandResult, int id) where T : ICommandResult, new()
        {
            return new T
            {
                Id = id,
                IsSuccessful = true
            };
        }
    }
}