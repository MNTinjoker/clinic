﻿namespace Clinic.Api.Application.DTOs.Contacts
{
    public class SaveContactDto
    {
        public int ContactTypeId { get; set; }
        public int? TitleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PreferredName { get; set; }
        public string? CompanyName { get; set; }
        public string? ProviderNumber { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? City { get; set; }
        public string? PostCode { get; set; }
        public int? CountryId { get; set; }
        public string? Notes { get; set; }
        public int? JobId { get; set; }
        public int EditOrNew { get; set; }
    }
}
