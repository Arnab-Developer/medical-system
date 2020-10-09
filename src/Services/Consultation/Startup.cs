using MediatR;
using MedicalSystem.Services.Consultation.Behaviours;
using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.Options;
using MedicalSystem.Services.Consultation.Queries;
using MedicalSystem.Services.Consultation.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MedicalSystem.Tests.Services.Consultation")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace MedicalSystem.Services.Consultation
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            AddDbContext(services);
            services.Configure<DatabaseOptions>(Configuration.GetSection(DatabaseOptions.CONNECTION_STRINGS));
            services.AddTransient(typeof(IConsultationQueries), typeof(ConsultationQueries));
            services.AddTransient(typeof(IDoctorQueries), typeof(DoctorQueries));
            services.AddTransient(typeof(IPatientQueries), typeof(PatientQueries));
            services.AddTransient(typeof(IConsultationRepository), typeof(ConsultationRepository));
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcServices.DoctorService>();
                endpoints.MapGrpcService<GrpcServices.PatientService>();
                endpoints.MapGrpcService<GrpcServices.ConsultationService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        private void AddDbContext(IServiceCollection services)
        {
            string consultationDbConnectionString = Configuration.GetConnectionString("ConsultationDbConnectionString");
            services.AddDbContext<ConsultationContext>(option => option.UseSqlServer(consultationDbConnectionString));
        }
    }
}
