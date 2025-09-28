﻿using AutoMapper;
using Clinic.Api.Application.DTOs;
using Clinic.Api.Application.DTOs.Appointments;
using Clinic.Api.Application.DTOs.Contacts;
using Clinic.Api.Application.DTOs.Invoices;
using Clinic.Api.Application.DTOs.Main;
using Clinic.Api.Application.DTOs.Patients;
using Clinic.Api.Application.DTOs.Questions;
using Clinic.Api.Application.DTOs.Users;
using Clinic.Api.Domain.Entities;

namespace Clinic.Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserContext, UserDto>().ReverseMap();
            CreateMap<LoginUserDto, UserContext>().ReverseMap();
            CreateMap<CreateAppointmentDto, AppointmentsContext>().ReverseMap();
            CreateMap<SavePatientDto, PatientsContext>().ReverseMap();
            CreateMap<SaveTreatmentDto, TreatmentsContext>().ReverseMap();
            CreateMap<SaveQuestionValueDto, QuestionValuesContext>().ReverseMap();
            CreateMap<SaveLoginHistoryDto, LoginHistoriesContext>().ReverseMap();
            CreateMap<SavePatientPhoneDto, PatientPhonesContext>().ReverseMap();
            CreateMap<SaveInvoiceDto, InvoicesContext>().ReverseMap();
            CreateMap<SaveInvoiceItemDto, InvoiceItemsContext>().ReverseMap();
            CreateMap<SaveReceiptDto, ReceiptsContext>().ReverseMap();
            CreateMap<SavePaymentDto, PaymentsContext>().ReverseMap();
            CreateMap<SaveJobDto, JobsContext>().ReverseMap();
            CreateMap<SaveContactDto, ContactsContext>().ReverseMap();
            CreateMap<SaveExpenseDto, ExpensesContext>().ReverseMap();
            CreateMap<SaveBillableItemsDto, BillableItemsContext>().ReverseMap();
            CreateMap<SaveContactPhoneDto, ContactPhonesContext>().ReverseMap();
            CreateMap<SaveProductDto, ProductsContext>().ReverseMap();
        }
    }
}
