using System.ComponentModel.DataAnnotations;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.SportAggregate;

namespace SportCity.Web.Models;

public record EventListResponse(int Id, int Capacity, int ParticipantsCount, DateTime DateTime, Category Category,
    Sport Sport, PlaygroundListResponse Playground);

public record EventResponse(int Id, int Capacity, int ParticipantsCount, DateTime DateTime, Category Category,
    Sport Sport, PlaygroundListResponse Playground, PlayerListResponse Organizer, List<PlayerListResponse> Participants);

public record EventAddParticipantRequest([Required] int PlayerId);
