using AutoMapper;
using Logistics.Domain;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Identity;
using Logistics.WebApi.Dto;
using static Logistics.WebApi.MenuRequest;

namespace Logistics.WebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Consignee, ConsigneeDto>();
            CreateMap<AddAndUpdateMenuRequest, Menu>();
            CreateMap<Menu, MenuItemDto>();
            CreateMap<Menu, MenuTreeDto>();
            CreateMap<Menu, MenuListDto>()
                .ForMember(dest => dest.Routes, opt => opt.MapFrom(src => src.Children));
        }
    }
}

