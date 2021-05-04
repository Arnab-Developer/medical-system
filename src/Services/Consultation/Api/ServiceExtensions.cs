﻿using MedicalSystem.Services.Consultation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MedicalSystem.Services.Consultation.Api
{
    internal static class ServiceExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string consultationDbConnectionString = configuration.GetConnectionString("ConsultationDbConnectionString");
            services.AddDbContext<ConsultationContext>(option => option.UseSqlServer(consultationDbConnectionString));
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(
                    configuration.GetConnectionString("ConsultationDbConnectionString"),
                    name: "ConsultationDbCheck",
                    tags: new string[] { "ConsultationDb" });

            return services;
        }
    }
}