using AutoMapper;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.User;
using SportCity.Web.Models;

namespace SportCity.Web.Mapping;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<City, CityResponse>();
        CreateMap<Sport, SportResponse>();
        CreateMap<Category, CategoryResponse>();
        CreateMap<User, UserCreateResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<Player, PlayerResponse>();
        CreateMap<Player, PlayerListResponse>();
        CreateMap<Playground, PlaygroundCreateResponse>();
        CreateMap<Playground, PlaygroundListResponse>();
        CreateMap<Playground, PlaygroundResponse>();

        CreateMap<Event, EventListResponse>();
        CreateMap<Event, EventResponse>();

        CreateMap<PlayerRequest, Player>();
        CreateMap<PlaygroundRequest, Playground>();

    }
}
