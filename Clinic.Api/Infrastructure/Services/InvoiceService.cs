﻿using AutoMapper;
using Clinic.Api.Application.DTOs;
using Clinic.Api.Application.DTOs.Invoices;
using Clinic.Api.Application.Interfaces;
using Clinic.Api.Domain.Entities;
using Clinic.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IReadTokenClaims _token;

        public InvoiceService(ApplicationDbContext context, IMapper mapper, IReadTokenClaims token)
        {
            _context = context;
            _mapper = mapper;
            _token = token;
        }

        public async Task<GlobalResponse> SaveInvoice(SaveInvoiceDto model)
        {
            var result = new GlobalResponse();

            try
            {
                var userId = _token.GetUserId();

                if (model.EditOrNew == -1)
                {
                    var invoice = _mapper.Map<InvoicesContext>(model);
                    invoice.CreatorId = userId;
                    invoice.CreatedOn = DateTime.UtcNow;
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync();
                    result.Data = $"Invoice Saved Successfully , Id : {invoice.Id}";
                    result.Status = 0;
                    return result;
                }
                else
                {
                    var existingInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == model.EditOrNew);

                    if (existingInvoice == null)
                    {
                        throw new Exception("Invoice Not Found");
                    }

                    _mapper.Map(model, existingInvoice);
                    existingInvoice.ModifierId = userId;
                    existingInvoice.LastUpdated = DateTime.UtcNow;
                    _context.Invoices.Update(existingInvoice);
                    await _context.SaveChangesAsync();
                    result.Data = $"Invoice Updated Successfully , Id : {existingInvoice.Id}";
                    result.Status = 0;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetInvoicesResponse>> GetInvoices()
        {
            try
            {
                var invoices = await (from i in _context.Invoices
                                      join p in _context.Patients on i.PatientId equals p.Id
                                      join b in _context.Businesses on i.BusinessId equals b.Id
                                      select new GetInvoicesResponse
                                      {
               Id = i.Id,
               InvoiceNo = i.InvoiceNo,
               BusinessId = i.BusinessId,
               IssueDate = i.IssueDate,
               PatientId = i.PatientId,
               PractitionerId = i.PractitionerId,
               AppointmentId = i.AppointmentId,
               InvoiceTo = i.InvoiceTo,
               ExtraPatientInfo = i.ExtraPatientInfo,
               TotalDiscount = i.TotalDiscount,
               Amount = i.Amount,
               Notes = i.Notes,
               Payment = i.Payment,
               ModifierId = i.ModifierId,
               CreatedOn = i.CreatedOn,
               LastUpdated = i.LastUpdated,
               InvoiceBillStatusId = i.InvoiceBillStatusId,
               AllowPayLater = i.AllowPayLater,
               UserAllowPayLaterId = i.UserAllowPayLaterId,
               Receipt = i.Receipt,
               BillStatus = i.BillStatus,
               IsCanceled = i.IsCanceled,
               BusinessDebit = i.BusinessDebit,
               CreatorId = i.CreatorId,
               RecordStateId = i.RecordStateId,
               AnesthesiaTechnicianId = i.AnesthesiaTechnicianId,
               ElectroTechnicianId = i.ElectroTechnicianId,
               IsFirstInvoice = i.IsFirstInvoice,
               Anesthesia = i.Anesthesia,
               BusinessAmount = i.BusinessAmount,
               AcceptDiscount = i.AcceptDiscount,
               AssistantId = i.AssistantId,
               PatientName = p.FirstName + " " + p.LastName,
               BusinessName = b.Name
           })
           .ToListAsync();

                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GlobalResponse> SaveInvoiceItem(SaveInvoiceItemDto model)
        {
            var result = new GlobalResponse();

            try
            {
                var userId = _token.GetUserId();

                if (model.EditOrNew == -1)
                {
                    var invoiceItem = _mapper.Map<InvoiceItemsContext>(model);
                    invoiceItem.CreatorId = userId;
                    invoiceItem.CreatedOn = DateTime.UtcNow;
                    _context.InvoiceItems.Add(invoiceItem);
                    await _context.SaveChangesAsync();
                    result.Data = "Invoice Item Saved Successfully";
                    result.Status = 0;
                    return result;
                }
                else
                {
                    var existingInvoiceItem = await _context.InvoiceItems.FirstOrDefaultAsync(i => i.Id == model.EditOrNew);

                    if (existingInvoiceItem == null)
                    {
                        throw new Exception("Invoice Item Not Found");
                    }

                    _mapper.Map(model, existingInvoiceItem);
                    existingInvoiceItem.ModifierId = userId;
                    existingInvoiceItem.LastUpdated = DateTime.UtcNow;
                    _context.InvoiceItems.Update(existingInvoiceItem);
                    await _context.SaveChangesAsync();
                    result.Data = "Invoice Item Updated Successfully";
                    result.Status = 0;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<InvoiceItemsContext>> GetInvoiceItems(int invoiceId)
        {
            try
            {
                var invoiceItems = await _context.InvoiceItems.Where(i => i.InvoiceId == invoiceId).ToListAsync();
                return invoiceItems;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GlobalResponse> DeleteInvoice(int id)
        {
            var result = new GlobalResponse();

            try
            {
                var invoice = await _context.Invoices.FindAsync(id);

                if (invoice == null)
                    throw new Exception("Invoice Not Found");

                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
                result.Data = "Invoice Deleted Successfully";
                result.Status = 0;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GlobalResponse> DeleteInvoiceItem(int id)
        {
            var result = new GlobalResponse();

            try
            {
                var invoiceItems = await _context.InvoiceItems.FindAsync(id);
                if (invoiceItems == null)
                    throw new Exception("Invoice Items Not Found");

                _context.InvoiceItems.Remove(invoiceItems);
                await _context.SaveChangesAsync();
                result.Data = "Invoice Item Deleted Successfully";
                result.Status = 0;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GlobalResponse> SaveReceipt(SaveReceiptDto model)
        {
            var result = new GlobalResponse();

            try
            {
                var userId = _token.GetUserId();

                var receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.PatientId == model.PatientId);

                if (receipt == null)
                {
                    var mappReceipt = _mapper.Map<ReceiptsContext>(model);
                    mappReceipt.CreatorId = userId;
                    mappReceipt.CreatedOn = DateTime.UtcNow;
                    _context.Receipts.Add(mappReceipt);
                    await _context.SaveChangesAsync();
                    result.Data = "Receipt Saved Successfully";
                    result.Status = 0;
                    return result;
                }

                _mapper.Map(model, receipt);
                receipt.ModifierId = userId;
                receipt.LastUpdated = DateTime.UtcNow;
                _context.Receipts.Update(receipt);
                await _context.SaveChangesAsync();
                result.Data = "Receipt Updated Successfully";
                result.Status = 0;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ReceiptsContext>> GetReceipts(int? patientId)
        {
            try
            {
                if (patientId == null)
                {
                    var receipts = await _context.Receipts.ToListAsync();
                    return receipts;
                }

                var receipt = await _context.Receipts.Where(r => r.PatientId == patientId).ToListAsync();
                return receipt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GlobalResponse> DeleteReceipt(int patientId)
        {
            var result = new GlobalResponse();

            try
            {
                var receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.PatientId == patientId);
                if (receipt == null)
                    throw new Exception("Receipt Not Found");

                _context.Receipts.Remove(receipt);
                await _context.SaveChangesAsync();
                result.Data = "Receipt Deleted Successfully";
                result.Status = 0;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GlobalResponse> SaveExpense(SaveExpenseDto model)
        {
            var result = new GlobalResponse();

            try
            {
                var userId = _token.GetUserId();

                if (model.EditOrNew == -1)
                {
                    var expense = _mapper.Map<ExpensesContext>(model);
                    expense.CreatedOn = DateTime.UtcNow;
                    expense.CreatorId = userId;
                    _context.Expenses.Add(expense);
                    await _context.SaveChangesAsync();
                    result.Data = "Expense Saved Successfully";
                    result.Status = 0;
                    return result;
                }
                else
                {
                    var existingExpense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == model.EditOrNew);
                    if (existingExpense == null)
                    {
                        throw new Exception("Expense Not Found");
                    }

                    _mapper.Map(model, existingExpense);
                    existingExpense.ModifierId = userId;
                    existingExpense.LastUpdated = DateTime.UtcNow;
                    _context.Expenses.Update(existingExpense);
                    await _context.SaveChangesAsync();
                    result.Data = "Expense Updated Successfully";
                    result.Status = 0;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ExpensesContext>> GetExpenses()
        {
            try
            {
                var expenses = await _context.Expenses.ToListAsync();
                return expenses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
