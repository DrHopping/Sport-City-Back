using System.ComponentModel.DataAnnotations;

namespace SportCity.Web.Models;

public record CategoryRequest([Required] string Name); 
public record CategoryResponse(int Id, string Name);
