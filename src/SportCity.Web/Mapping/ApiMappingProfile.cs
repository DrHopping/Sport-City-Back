using AutoMapper;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Web.Models;

namespace SportCity.Web.Mapping;

public class ApiMappingProfile : Profile
{
  public ApiMappingProfile()
  {
    CreateMap<City, CityResponse>();
  }
}
