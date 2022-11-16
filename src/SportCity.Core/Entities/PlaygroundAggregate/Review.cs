using SportCity.Core.Entities.PlayerAggregate;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.PlaygroundAggregate;

public class Review : BaseEntity
{
    public double Rating { get; private set; }
    public string Comment { get; private set; }
    public int ReviewerId { get; private set; }
    public int PlaygroundId { get; private set; }
    public Player Reviewer { get; private set; }

    private Review() { }

    public Review(double rating, string comment, int reviewer, int playground)
    {
        Rating = rating;
        Comment = comment;
        ReviewerId = reviewer;
        PlaygroundId = playground;
    }
}
