using SportCity.Core.Entities.PlayerAggregate;
using SportCity.SharedKernel;
using SportCity.SharedKernel.Interfaces;

namespace SportCity.Core.Entities.PlaygroundAggregate;

public class Review : BaseEntity
{
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public int ReviewerId { get; private set; }

    public Player Reviewer { get; private set; }

    private Review() { }

    public Review(int rating, string comment, int reviewer)
    {
        Rating = rating;
        Comment = comment;
        ReviewerId = reviewer;
    }
}
