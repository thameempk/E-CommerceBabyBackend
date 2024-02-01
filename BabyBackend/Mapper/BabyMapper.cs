﻿using AutoMapper;
using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Mapper
{
    public class BabyMapper : Profile
    {
        public BabyMapper()
        {
            CreateMap<Users, UserRegisterDto>().ReverseMap();
        }
    }
}
