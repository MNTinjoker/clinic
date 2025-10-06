﻿using Clinic.Api.Application.DTOs;
using Clinic.Api.Application.DTOs.Treatments;
using Clinic.Api.Domain.Entities;

namespace Clinic.Api.Application.Interfaces
{
    public interface ITreatmentService
    {
        Task<GlobalResponse> CreateAppointmentAsync(CreateAppointmentDto model);
        Task<GlobalResponse> DeleteAppointment(int id);
        Task<IEnumerable<AppointmentsContext>> GetAppointments(GetAppointmentsDto model);
        Task<IEnumerable<TreatmentsContext>> GetTreatments(int appointmentId);
        Task<GlobalResponse> SaveTreatment(SaveTreatmentDto model);
        Task<GlobalResponse> DeleteTreatment(int id);
        Task<IEnumerable<GetTodayAppointmentsInfoDto>> GetTodayAppointments(GetTodayAppointmentsDto model);
        Task<IEnumerable<GetAppointmentTypesDto>> GetAppointmentTypes();
        Task<List<GetTodayAppointmentsInfoDto>> GetWeekAppointments();
        Task<IEnumerable<BillableItemsContext>> GetBillableItems();
        Task<GlobalResponse> SaveBillableItem(SaveBillableItemsDto model);
        Task<GlobalResponse> DeleteBillableItem(int id);
        Task<IEnumerable<SectionsContext>> GetSectionPerService(int serviceId);
        Task<IEnumerable<QuestionsContext>> GetQuestionsPerSection(int sectionId);
        Task<IEnumerable<AnswersContext>> GetAnswersPerQuestion(int questionId);
        Task<IEnumerable<GetServicesPerPatientResponse>> GetPatientServices(int patinetId);
        Task<IEnumerable<GetPatientTreatmentsResponse>> GetPatientTreatments(int patientId);
    }
}
