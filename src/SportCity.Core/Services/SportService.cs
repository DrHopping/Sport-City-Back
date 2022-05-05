using Ardalis.GuardClauses;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.Entities.SportAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class SportService : ISportService
{
  private readonly IRepository<Sport> _sportRepository;

  public SportService(IRepository<Sport> sportRepository)
  {
    _sportRepository = sportRepository;
  }

  public async Task<Sport> CreateSport(string name)
  {
    var sport = await _sportRepository.GetBySpecAsync(new SportByNameSpec(name));
    Guard.Against.EntityAlreadyExists(sport, nameof(name), name);
    
    sport = new Sport(name);
    await _sportRepository.AddAsync(sport);
    return sport;
  }
  
  public async Task<List<Sport>> GetAllSports() => await _sportRepository.ListAsync();
  
  public async Task<Sport> GetSportById(int id)
  {
    var sport = await _sportRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(sport, nameof(id), id.ToString());
    return sport;
  }
  
  public async Task<Sport> UpdateSportName(int id, string name)
  {
    var sport = await _sportRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(sport, nameof(id), id.ToString());    
    
    var sameNameSport = await _sportRepository.GetBySpecAsync(new SportByNameSpec(name));
    Guard.Against.EntityAlreadyExists(sameNameSport, nameof(name), name);

    sport.UpdateName(name);        
    await _sportRepository.UpdateAsync(sport);
    return sport;
  }
 
  public async Task DeleteSport(int id)
  {
    var sport = await _sportRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(sport, nameof(id), id.ToString());    
    await _sportRepository.DeleteAsync(sport);
  }
  
}

public interface ISportService
{
  Task<Sport> CreateSport(string name);
  Task<List<Sport>> GetAllSports();
  Task<Sport> GetSportById(int id);
  Task<Sport> UpdateSportName(int id, string name);
  Task DeleteSport(int id);
}
