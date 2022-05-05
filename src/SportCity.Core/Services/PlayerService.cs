using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlayerAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class PlayerService : IPlayerService
{
  private readonly IRepository<Player> _playerRepository;
  private readonly ICategoryService _categoryService;

  public PlayerService(IRepository<Player> playerRepository, ICategoryService categoryService)
  {
    _playerRepository = playerRepository;
    _categoryService = categoryService;
  }

  public async Task<List<Player>> GetAllPlayers() => await _playerRepository.ListAsync(new PlayerIncludeCategorySpec());

  public async Task<Player> GetPlayerById(string id)
  {
    var player = await _playerRepository.GetBySpecAsync(new PlayerByIdSpec(id));
    Guard.Against.EntityNotFound(player, nameof(id), id);
    return player;
  }

  public async Task<Player> UpdatePlayer(string id, Player playerUpdate)
  {
    var player = await GetPlayerById(id);
    await _categoryService.GetCategoryById(player.CategoryId);
    player.UpdateWith(playerUpdate);
    _playerRepository.
    
    
    return player;
  }
  
}

public interface IPlayerService
{
  Task<List<Player>> GetAllPlayers();
  Task<Player> GetPlayerById(string id);
}
