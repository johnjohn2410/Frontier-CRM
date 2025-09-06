using AutoMapper;
using FrontierCRM.Application.Common.DTOs;
using FrontierCRM.Domain.Entities;
using FrontierCRM.Domain.ValueObjects;

namespace FrontierCRM.Application.Common.Mappings;

/// <summary>
/// AutoMapper profile for mapping between entities and DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tenant, TenantDto>();
        CreateMap<TenantSettings, TenantSettingsDto>();
        CreateMap<TenantSettingsDto, TenantSettings>();
        
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));
        CreateMap<UserPreferences, UserPreferencesDto>();
        CreateMap<UserPreferencesDto, UserPreferences>();
        
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission)));
    }
}
