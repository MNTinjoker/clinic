﻿namespace Clinic.Api.Application.DTOs
{
    public class GetAllPaymentsResponse
    {
        public int Id { get; set; }
        public string? PaymentNo { get; set; }
        public string? PatientName { get; set; }
        public int PatientId { get; set; }
        public decimal? Cash { get; set; }
        public decimal? EFTPos { get; set; }
        public decimal? Other { get; set; }
        public string? Notes { get; set; }
        public int? ModifierId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool AllowEdit { get; set; }
        public int? CreatorId { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
