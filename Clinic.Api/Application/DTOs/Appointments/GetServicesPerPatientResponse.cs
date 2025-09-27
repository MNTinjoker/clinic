﻿namespace Clinic.Api.Application.DTOs.Appointments
{
    public class GetServicesPerPatientResponse
    {
        public int InvoiceId { get; set; }
        public int InvoiceItemId { get; set; }
        public string? BillableItemName { get; set; }
        public decimal? BillableItemPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
