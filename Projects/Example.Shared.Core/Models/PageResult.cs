using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Example.Shared.Core.Models
{
    [JsonObject]
    public class PageResult<T> : IEnumerable<T>
    {
        [JsonProperty("@odata.nextLink")]
        public Uri NextPageLink { get; private set; }
        [JsonProperty("@odata.count")]
        public long? Count { get; private set; }
        [JsonProperty("value")]
        public IEnumerable<T> Items { get; private set; }

        public PageResult(IEnumerable<T> items, Uri nextPageLink, long? count)
        {
            NextPageLink = nextPageLink;
            Count = count;
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
