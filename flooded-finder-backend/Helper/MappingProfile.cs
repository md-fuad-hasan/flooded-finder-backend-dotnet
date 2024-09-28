using AutoMapper;
using flooded_finder_backend.Dto;
using flooded_finder_backend.Models;

namespace flooded_finder_backend.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Division, DivisionDto>();
            CreateMap<DivisionDto, Division>();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictDto, District>();
            CreateMap<Upazila, UpazilaDto>();
            CreateMap<UpazilaDto, Upazila>();
            CreateMap<Area, AreaDto>();
            CreateMap<AreaDto, Area>();
        }
    }
}
