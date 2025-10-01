﻿using Clinic.Api.Application.DTOs;
using Clinic.Api.Application.DTOs.Patients;
using Clinic.Api.Application.Interfaces;
using Clinic.Api.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("savePatient")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> SavePatient(SavePatientDto model)
        {
            var result = await _patientService.SavePatient(model);
            return Ok(result);
        }

        [HttpGet("deletePatient/{id}")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatient(id);
            return Ok(result);
        }

        [HttpGet("getPatients")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> GetPatients()
        {
            var result = await _patientService.GetPatients();

            return Ok(result);
        }

        [HttpGet("getPatientById/{patientId}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetPatientById(int patientId)
        {
            var result = await _patientService.GetPatientById(patientId);
            return Ok(result);
        }

        [HttpPost("savePatientPhone")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> SavePatientPhone(SavePatientPhoneDto model)
        {
            var result = await _patientService.SavePatientPhone(model);
            return Ok(result);
        }

        [HttpGet("deletePatientPhone/{id}")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> DeletePatientPhone(int id)
        {
            var result = await _patientService.DeletePatientPhone(id);
            return Ok(result);
        }

        [HttpGet("getPatientPhone/{patientId}")]
        [Authorize("Admin", "Doctor")]
        public async Task<IActionResult> GetPatientPhone(int patientId)
        {
            var result = await _patientService.GetPatientPhones(patientId);
            return Ok(result);
        }

        [HttpGet("getPatientAppointments/{patientId}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetPatientAppointments(int patientId)
        {
            var result = await _patientService.GetPatientAppointments(patientId);
            return Ok(result);
        }

        [HttpGet("getPatientInvoices/{patientId}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetPatientInvoices(int patientId)
        {
            var result = await _patientService.GetPatientInvoices(patientId);
            return Ok(result);
        }

        [HttpGet("getPatientReceipts/{patientId}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetPatientReceipts(int patientId)
        {
            var result = await _patientService.GetPatientReceipts(patientId);
            return Ok(result);
        }

        [HttpGet("getPatientPayments/{patientId}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetPatientPayments(int patientId)
        {
            var result = await _patientService.GetPatientPayments(patientId);
            return Ok(result);
        }

        [HttpPost("saveAttachment")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> SaveAttachment(SaveAttachmentsDto model)
        {
            var result = await _patientService.SaveAttachment(model);
            return Ok(result);
        }

        [HttpGet("getAttachment/{patientId}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> GetAttachment(int patientId)
        {
            var result = await _patientService.GetAttachment(patientId);
            return Ok(result);
        }

        [HttpGet("deleteAttachment/{id}")]
        [Authorize("Admin","Doctor")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var result = await _patientService.DeleteAttachment(id);
            return Ok(result);
        }
    }
}
