using System.Reflection;
using AutoMapper;
using Example.App.Common;
using Example.App.CQS.Queries;
using Example.App.Data.Context;
using Example.Core.Extensions;
using Example.Core.WebApi;
using FluentValidation.AspNetCore;
using Flurl.Http;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Example.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(WebAppDefaults.SetMvcNewtonsoftJsonOptions)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddAutoMapper(typeof(Startup));

            services.AddHttpClient();
            FlurlHttp.ConfigureClient(GetFakeNamesQueryHandler.FakeNameUrl, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.OperationFilter<CancellationTokenOperationFilter>();
            });

            services.AddDbContext<ContactDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ContactContext")));
            services.AddScoped<IContactsDbContext, ContactDbContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddOData();
            services.AddODataSupportedMediaTypes();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.EnsureMigrationOfContext<ContactDbContext>();

            app.UseODataMvc(EdmModelBuilder.GetEdmModel());
        }
    }
}
