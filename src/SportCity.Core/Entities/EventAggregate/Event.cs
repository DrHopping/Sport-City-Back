﻿using Ardalis.GuardClauses;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.Core.Guards;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.EventAggregate;

public class Event : BaseEntity, IAggregateRoot
{
  public int CategoryId { get; private set; }
  public Category Category { get; private set; }
  public int SportId { get; private set; }
  public Sport Sport { get; private set; }
  public int Capacity { get; private set; }
  public DateTime DateTime { get; private set; }
  public int OrganizerId { get; private set; }
  public Player Organizer { get; private set; }

  private readonly List<Player> _participants = new();
  public IReadOnlyCollection<Player> Participants => _participants.AsReadOnly();

  private Event() { }

  public Event(int category, int sport, int capacity, DateTime dateTime, int organizer)
  {
    CategoryId = category;
    SportId = sport;
    Capacity = capacity;
    DateTime = dateTime;
    OrganizerId = organizer;
  }

  public void AddParticipant(Player player)
  {
    Guard.Against.FullEvent(_participants.Count, Capacity);
    _participants.Add(player);
  }
  
}