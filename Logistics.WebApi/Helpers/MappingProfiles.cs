﻿using AutoMapper;
using Logistics.Domain;
using Logistics.Domain.Entities;
using Logistics.WebApi.Dto;
using static Logistics.WebApi.MenuRequest;

namespace Logistics.WebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Consignee, ConsigneeDto>();
            CreateMap<Menu, MenuListDto>()
                .ForMember(dest => dest.Routes, opt => opt.MapFrom(src => src.Children));
            CreateMap<AddAndUpdateMenuRequest, Menu>();
        }
    }
}
