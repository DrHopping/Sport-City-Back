namespace SportCity.Core.Interfaces;

public interface IAuthorizationService
{
  bool IsAdmin();
  void SetToken(string token);
  string GetIdentity();
}
