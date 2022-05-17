namespace SportCity.Web.Models;

public record PlayerResponse(int Id, string FirstName, string LastName, CategoryResponse Category);

public record PlayerRequest(string FirstName, string LastName, int CategoryId);
