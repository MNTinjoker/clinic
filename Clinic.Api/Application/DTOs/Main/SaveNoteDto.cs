﻿namespace Clinic.Api.Application.DTOs.Main
{
    public class SaveNoteDto
    {
        public string? Message { get; set; }
        public int PatientId { get; set; }
        public int EditOrNew { get; set; }
        public int? OrderOf { get; set; }
    }
}
