﻿namespace Clinic.Api.Domain.Entities
{
    public class InvoiceItemsContext
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int? ItemId { get; set; }
        public int? ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public int DiscountTypeId { get; set; }
        public decimal Amount { get; set; }
        public int? ModifierId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool IsLock { get; set; }
        public int? CreatorId { get; set; }
        public bool Done { get; set; }
    }
}
