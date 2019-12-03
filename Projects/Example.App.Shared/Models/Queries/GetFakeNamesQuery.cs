using System.Collections.Generic;
using Example.App.Shared.Models.View;

namespace Example.App.Shared.Models.Queries
{
    public class GetFakeNamesResult 
    {
        public List<FakeName> FakeNames { get; set; }
        public int FakeNamesCount { get; set; }
    }
}
