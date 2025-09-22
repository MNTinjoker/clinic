﻿using Clinic.Api.Application.DTOs;
using Clinic.Api.Application.DTOs.Invoices;
using Clinic.Api.Domain.Entities;

namespace Clinic.Api.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<GlobalResponse> SaveInvoice(SaveInvoiceDto model);
        Task<IEnumerable<GetInvoicesResponse>> GetInvoices();
        Task<GlobalResponse> SaveInvoiceItem(SaveInvoiceItemDto model);
        Task<IEnumerable<InvoiceItemsContext>> GetInvoiceItems(int invoiceId);
        Task<GlobalResponse> DeleteInvoice(int id);
        Task<GlobalResponse> DeleteInvoiceItem(int id);
        Task<GlobalResponse> SaveReceipt(SaveReceiptDto model);
        Task<IEnumerable<ReceiptsContext>> GetReceipts(int? patientId);
        Task<GlobalResponse> DeleteReceipt(int patientId);
        Task<GlobalResponse> SaveExpense(SaveExpenseDto model);
        Task<IEnumerable<ExpensesContext>> GetExpenses();
    }
}
