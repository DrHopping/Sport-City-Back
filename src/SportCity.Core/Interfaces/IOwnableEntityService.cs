namespace SportCity.Core.Interfaces;

public interface IOwnableEntityService
{
    Task<string> GetOwnerId(int id);
}
