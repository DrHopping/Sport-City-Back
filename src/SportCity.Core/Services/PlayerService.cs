using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlayerAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.Core.Interfaces;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class PlayerService : IPlayerService
{
  private readonly IRepository<Player> _playerRepository;
  private readonly IRepository<Category> _categoryRepository;

  public PlayerService(IRepository<Player> playerRepository)
  {
    _playerRepository = playerRepository;
  }

  public async Task<List<Player>> GetAllPlayers() => await _playerRepository.ListAsync(new PlayerIncludeCategorySpec());

  public async Task<Player> GetPlayerById(string id)
  {
    var player = await _playerRepository.GetBySpecAsync(new PlayerByAnyIdSpec(id));
    Guard.Against.EntityNotFound(player, nameof(id), id);
    return player;
  }

  public async Task<Player> UpdatePlayer(int id, Player playerUpdate)
  {
    var player = await _playerRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(player, nameof(id), id.ToString());
    
    var category = await _categoryRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(category, nameof(id), id.ToString());
    
    player.UpdateWith(playerUpdate);
    await _playerRepository.UpdateAsync(player);
    return player;
  }

  public async Task<string> GetOwnerId(int id)
  {
    var player = await _playerRepository.GetByIdAsync(id);
    Guard.Against.EntityNotFound(player, nameof(id), id.ToString());
    return player.IdentityGuid;
  }
}

public interface IPlayerService : IOwnableEntityService
{
  Task<List<Player>> GetAllPlayers();
  Task<Player> GetPlayerById(string id);
  Task<Player> UpdatePlayer(int id, Player playerUpdate);
}
