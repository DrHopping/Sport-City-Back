using Ardalis.GuardClauses;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Entities.PlaygroundAggregate.Specifications;
using SportCity.Core.Guards;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Services;

public class PlaygroundService : IPlaygroundService
{
    private readonly IRepository<Playground> _playgroundRepository;
    private readonly IRepository<City> _cityRepository;

    public PlaygroundService(IRepository<Playground> playgroundRepository, IRepository<City> cityRepository)
    {
        _playgroundRepository = playgroundRepository;
        _cityRepository = cityRepository;
    }

    public async Task<Playground> CreatePlayground(string name, string description, int cityId, string photoUrl,
        Location location)
    {
        var playground = new Playground(name, description, cityId, photoUrl, location);
        playground = await _playgroundRepository.AddAsync(playground);
        return playground;
    }

    public async Task<List<Playground>> GetAllPlaygrounds() =>
        await _playgroundRepository.ListAsync(new PlaygroundListSpec());

    public async Task<List<Playground>> GetCityPlaygrounds(int cityId) =>
        await _playgroundRepository.ListAsync(new PlaygroundListByCitySpec(cityId));

    public async Task<Playground> GetPlaygroundById(int id)
    {
        var playground = await _playgroundRepository.GetBySpecAsync(new PlaygroundByIdSpec(id));
        Guard.Against.EntityNotFound(playground, nameof(id), id.ToString());
        return playground;
    }

    public async Task<Playground> UpdatePlayground(int id, Playground playgroundUpdate)
    {
        var playground = await _playgroundRepository.GetByIdAsync(id);
        Guard.Against.EntityNotFound(playground, nameof(id), id.ToString());

        var city = await _cityRepository.GetByIdAsync(playgroundUpdate.CityId);
        Guard.Against.EntityNotFound(city, nameof(playgroundUpdate.CityId), playgroundUpdate.CityId.ToString());

        playground.UpdateWith(playgroundUpdate);
        await _playgroundRepository.UpdateAsync(playground);
        return playground;
    }

    public async Task DeletePlayground(int id)
    {
        var playground = await _playgroundRepository.GetByIdAsync(id);
        Guard.Against.EntityNotFound(playground, nameof(id), id.ToString());
        await _playgroundRepository.DeleteAsync(playground);
    }
}

public interface IPlaygroundService
{
    Task<Playground> CreatePlayground(string name, string description, int cityId, string photoUrl, Location location);
    Task<List<Playground>> GetAllPlaygrounds();
    Task<List<Playground>> GetCityPlaygrounds(int cityId);
    Task<Playground> GetPlaygroundById(int id);
    Task<Playground> UpdatePlayground(int id, Playground playgroundUpdate);
    Task DeletePlayground(int id);
}
