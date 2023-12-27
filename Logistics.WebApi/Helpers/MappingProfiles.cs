using AutoMapper;
using Logistics.Domain;
using Logistics.Domain.Entities;
using Logistics.WebApi.Dto;
using static Logistics.WebApi.MenuRequest;
using static Logistics.WebApi.PermissionRequest;

namespace Logistics.WebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<AddAndUpdateMenuRequest, Menu>();
            CreateMap<Menu, MenuItemDto>();
            CreateMap<Menu, MenuTreeDto>();
            CreateMap<Menu, MenuListDto>()
                .ForMember(dest => dest.Routes, opt => opt.MapFrom(src => src.Children));

            CreateMap<Consignee, ConsigneeDto>();
            CreateMap<Permission, PermissionListDto>();
            CreateMap<PermissionAddAndUpdateRequest, Permission>()
                .ForMember(dest => dest.HttpMethod, opt => opt.MapFrom(src => string.Join(",", src.HttpMethod)))
                .ForMember(dest => dest.HttpPath, opt => opt.MapFrom(src => string.Join(",", src.HttpPath)));
        }
    }
}

