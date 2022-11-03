using AutoMapper;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.User;
using SportCity.Infrastructure.Identity;

namespace SportCity.Infrastructure.Mapping;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<EfApplicationUser, User>().ForMember(u => u.IdentityId, opt => opt.MapFrom(au => au.Id));
    }
}
