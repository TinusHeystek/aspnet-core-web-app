using Example.Shared.Core.Models;

namespace Example.App.Data.Models
{
    public class Pet : IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int OwnerId { get; set; }
        public Contact Owner { get; set; }
    }
}