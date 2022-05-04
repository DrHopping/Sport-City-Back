using Ardalis.GuardClauses;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.CityAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class CityService : ICityService
{
  private readonly IRepository<City> _cityRepository;

  public CityService(IRepository<City> cityRepository)
  {
    _cityRepository = cityRepository;
  }

  public async Task<City> CreateCity(string name)
  {
    var sameNameCity = await _cityRepository.GetBySpecAsync(new CityByNameSpec(name));
    Guard.Against.EntityAlreadyExists(sameNameCity, nameof(name), name);
    
    var city = new City(name);
    await _cityRepository.AddAsync(city);
    return city;
  }
  
  public async Task<List<City>> GetAllCities() => await _cityRepository.ListAsync();
  
  public async Task<City> UpdateCityName(int id, string name)
  {
    var city = await _cityRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(city, nameof(id), id.ToString());
    
    var sameNameCity = await _cityRepository.GetBySpecAsync(new CityByNameSpec(name));
    Guard.Against.EntityAlreadyExists(sameNameCity, nameof(name), name);

    city.UpdateName(name);        
    await _cityRepository.UpdateAsync(city);
    return city;
  }
 
  public async Task DeleteCity(int id)
  {
    var city = await _cityRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(city, nameof(id), id.ToString());    
    await _cityRepository.DeleteAsync(city);
  }
  
}

public interface ICityService
{
  Task<City> CreateCity(string name);
  Task<List<City>> GetAllCities();
  Task<City> UpdateCityName(int id, string name);
  Task DeleteCity(int id);
}
