using FlavorTalk.Shared.Extensions;

namespace FlavorTalk.Domain.Entities;
public class Plate : BaseEntity
{
    private Plate() { }
    public Plate(string name, string? description)
    {
        name.CannotBeNullOrEmpty();
        Name = name;
        Description = description;
    }

    public string Name { get; set; }

    public string? Description { get; set; }

    public List<Review> Reviews { get; set; } = [];

    public float? RatingAverage { get; set; }
}
