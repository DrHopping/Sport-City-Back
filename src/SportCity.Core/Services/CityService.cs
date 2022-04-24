using Ardalis.GuardClauses;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.CityAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class CityService : ICityService
{
  private readonly IRepository<City> _basketRepository;
  private readonly IAppLogger<CityService> _logger;

  public CityService(IRepository<City> basketRepository, IAppLogger<CityService> logger)
  {
    _basketRepository = basketRepository;
    _logger = logger;
  }

  public async Task<City> CreateCity(string name)
  {
    var citySpec = new CityByNameSpec(name);
    var city = await _basketRepository.GetBySpecAsync(citySpec);
    Guard.Against.ExistingCity(city, name);
    
    city = new City(name);
    await _basketRepository.AddAsync(city);
    return city;
  }
}

public interface ICityService
{
  Task<City> CreateCity(string name);
}
