using System.ComponentModel.DataAnnotations;

namespace SportCity.Web.Models;

public record SportRequest([Required] string Name); 
public record SportResponse(int Id, string Name);
