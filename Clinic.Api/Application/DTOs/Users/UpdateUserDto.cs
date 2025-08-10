﻿namespace Clinic.Api.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public int Id { get; set; } 
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}
