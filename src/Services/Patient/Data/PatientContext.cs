﻿using MedicalSystem.Services.Patient.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patient.Data
{
    /// <include file='docs.xml' path='docs/members[@name="PatientContext"]/patientContext/*'/>
    public class PatientContext : DbContext
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientContext"]/patientContextConstructor/*'/>
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientContext"]/patients/*'/>
        public DbSet<PatientModel>? Patients { get; set; }
    }
}