using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Example.Core.WebApi
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BodyContentTypeAttribute : Attribute
    {
        public BodyContentTypeAttribute(params string[] contentTypes)
        {
            if (contentTypes == null || !contentTypes.Any())
                throw new ArgumentNullException(nameof(contentTypes));

            ContentTypes = new MediaTypeCollection();
            contentTypes.ToList()
                .ForEach(x =>
                {
                    ContentTypes.Add(MediaTypeHeaderValue.Parse(x));
                });
        }

        public MediaTypeCollection ContentTypes { get; }

        public bool Exclusive { get; set; }
    }
}