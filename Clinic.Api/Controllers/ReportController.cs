﻿using Clinic.Api.Application.DTOs;
using Clinic.Api.Application.DTOs.Report;
using Clinic.Api.Application.Interfaces;
using Clinic.Api.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("getInvoicesByClinic")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> GetInvoicesByClinic(InvoiceFilterDto model)
        {
            var result = await _reportService.GetInvoicesByClinic(model);
            return Ok(result);
        }

        [HttpPost("getInvoicesByService")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> GetInvoicesByService(InvoiceFilterDto model)
        {
            var result = await _reportService.GetInvoicesByService(model);
            return Ok(result);
        }

        [HttpPost("getAppointmentsAndUnpaidInvoices")]
        [Authorize("Doctor", "Admin")]
        public async Task<IActionResult> GetAppointmentsAndUnpaidInvoices(InvoiceFilterDto model)
        {
            var result = await _reportService.GetAppointmentsAndUnpaidInvoices(model);
            return Ok(result);
        }

        [HttpPost("getSubmitedInvoices")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> GetSubmitedInvoices(InvoiceFilterDto model)
        {
            var result = await _reportService.GetSubmitedInvoices(model);
            return Ok(result);
        }

        [HttpPost("getUnpaidInvoices")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetUnpaidInvoices(InvoiceFilterDto model)
        {
            var result = await _reportService.GetUnpaidInvoices(model);
            return Ok(result);
        }

        [HttpPost("getPractitionerIncome")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetPractitionerIncome(IncomeReportFilterDto model)
        {
            var result = await _reportService.GetPractitionerIncome(model);
            return Ok(result);
        }

        [HttpPost("getBusinessIncome")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> GetBusinessIncome(IncomeReportFilterDto model)
        {
            var result = await _reportService.GetBusinessIncome(model);
            return Ok(result);
        }

        [HttpPost("getIncomeReportDetails")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetIncomeReportDetails(IncomeReportFilterDto model)
        {
            var result = await _reportService.GetIncomeReportDetails(model);
            return Ok(result);
        }
    }
}
