namespace SportCity.Core.Interfaces;

public interface IUserService
{
  Task<User.User> CreateUser(string firstName, string lastName, string password, string email);
  Task PromoteUser(string id);
  Task DemoteUser(string id);
  Task<List<User.User>> GetAllUsers();

}
