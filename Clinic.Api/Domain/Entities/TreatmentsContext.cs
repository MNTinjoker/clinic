﻿namespace Clinic.Api.Domain.Entities
{
    public class TreatmentsContext
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int? AppointmentId { get; set; }
        public bool IsFinal { get; set; }
        public int TreatmentTemplateId { get; set; }
        public int? ModifierId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? CreatorId { get; set; }
        public int? InvoiceItemId { get; set; }
        public DateTime? VisitTime { get; set; }
    }
}
