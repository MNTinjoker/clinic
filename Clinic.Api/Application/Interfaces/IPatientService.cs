﻿using Clinic.Api.Application.DTOs.Patients;
using Clinic.Api.Domain.Entities;

namespace Clinic.Api.Application.Interfaces
{
    public interface IPatientService
    {
        Task<string> SavePatient(SavePatientDto model);
        Task<IEnumerable<PatientsContext>> GetPatients();
        Task<string> SavePatientPhone(SavePatientPhoneDto model);
        Task<IEnumerable<PatientPhonesContext>> GetPatientPhones(int patientId);
    }
}
