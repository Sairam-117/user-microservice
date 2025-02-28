using AutoMapper;
using eCommerce.Core.DTO;

namespace eCommerce.Core.Mappers;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() 
    {
        CreateMap<ApplicationUser, AuthenticationResponse>().ReverseMap();
        CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
    }
}
