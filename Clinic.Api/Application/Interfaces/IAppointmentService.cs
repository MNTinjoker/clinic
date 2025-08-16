﻿using Clinic.Api.Application.DTOs.Appointments;
using Clinic.Api.Domain.Entities;

namespace Clinic.Api.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<int> CreateAppointmentAsync(CreateAppointmentDto dto);
        Task<IEnumerable<AppointmentsContext>> GetAppointments(int clinicId, DateTime? date);
    }
}
