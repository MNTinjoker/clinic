﻿using AutoMapper;
using Clinic.Api.Application.DTOs.Invoices;
using Clinic.Api.Application.Interfaces;
using Clinic.Api.Domain.Entities;
using Clinic.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Api.Infrastructure.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IReadTokenClaims _token;

        public InvoicesService(ApplicationDbContext context, IMapper mapper, IReadTokenClaims token)
        {
            _context = context;
            _mapper = mapper;
            _token = token;
        }

        public async Task<string> SaveInvoices(SaveInvoicesDto model)
        {
            try
            {
                if (model.EditOrNew == -1)
                {
                    var invoice = _mapper.Map<InvoicesContext>(model);
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync();
                    return "Invoice Saved Successfully";
                }
                else
                { 
                    var existingInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == model.EditOrNew);

                    if (existingInvoice == null)
                    {
                        throw new Exception("Invoice Not Found");
                    }

                    _mapper.Map(model, existingInvoice);
                    _context.Invoices.Update(existingInvoice);
                    await _context.SaveChangesAsync();
                    return "Invoice Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<InvoicesContext>> GetInvoices()
        {
            try
            {
                var invoices = await _context.Invoices.ToListAsync();
                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveInvoiceItems(SaveInvoiceItemsDto model)
        {
            try
            {
                if (model.EditOrNew == -1)
                {
                    var invoiceItem = _mapper.Map<InvoiceItemsContext>(model);
                    _context.InvoiceItems.Add(invoiceItem);
                    await _context.SaveChangesAsync();
                    return "Invoice Item Saved Successfully";
                }
                else
                {
                    var existingInvoiceItem = await _context.InvoiceItems.FirstOrDefaultAsync(i => i.Id == model.EditOrNew);

                    if (existingInvoiceItem == null)
                    {
                        throw new Exception("Invoice Item Not Found");
                    }

                    _mapper.Map(model, existingInvoiceItem);
                    _context.InvoiceItems.Update(existingInvoiceItem);
                    await _context.SaveChangesAsync();
                    return "Invoice Item Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<InvoiceItemsContext>> GetInvoiceItems()
        {
            try
            {
                var invoiceItems = await _context.InvoiceItems.ToListAsync();
                return invoiceItems;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
