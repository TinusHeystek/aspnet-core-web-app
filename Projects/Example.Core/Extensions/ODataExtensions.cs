using System;
using System.Linq;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;

// ReSharper disable StringLiteralTypo

namespace Example.Core.Extensions
{
    public static class ODataExtensions
    {
        public static IMvcCoreBuilder AddODataSupportedMediaTypes(this IServiceCollection services)
        {
            // Workaround: https://github.com/OData/WebApi/issues/1177
            return services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
        }

        public static IApplicationBuilder UseODataMvc(this IApplicationBuilder app, IEdmModel model)
        {
            return app.UseMvc(builder =>
            {
                builder.SetTimeZoneInfo(TimeZoneInfo.Utc);
                builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                builder.MapODataServiceRoute("odata", "odata", model);
            });
        }
    }
}