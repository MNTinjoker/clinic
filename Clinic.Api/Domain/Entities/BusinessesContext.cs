﻿namespace Clinic.Api.Domain.Entities
{
    public class BusinessesContext
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostCode { get; set; }
        public int? CountryId { get; set; }
        public string? ContactInformation { get; set; }
        public bool DisplayInOnlineBooking { get; set; }
        public int? ModifierId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? Location { get; set; }
        public int? Zoom { get; set; }
        public string? InfoEmail { get; set; }
        public bool IsServiceBase { get; set; }
        public int? CreatorId { get; set; }
        public bool ShowInvoiceInRecord { get; set; }
        public bool CheckScheduleOnInvoice { get; set; }
        public bool IsInPatient { get; set; }
        public bool SMSEnabled { get; set; }
        public bool? AppointmentByOutOfRange { get; set; }
    }
}
