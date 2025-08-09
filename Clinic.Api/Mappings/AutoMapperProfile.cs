﻿using AutoMapper;
using Clinic.Api.Application.DTOs.Users;
using Clinic.Api.Domain.Entities;

namespace Clinic.Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserContext, UserDto>().ReverseMap();
            CreateMap<RegisterUserDto, UserContext>();
            CreateMap<LoginUserDto, UserContext>();
        }
    }
}
