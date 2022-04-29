using AutoMapper;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Web.Models;

namespace SportCity.Web.Mapping;

public class ApiMappingProfile : Profile
{
  public ApiMappingProfile()
  {
    CreateMap<City, CityResponse>();
    CreateMap<Sport, SportResponse>();
    CreateMap<Category, CategoryResponse>();
  }
}
