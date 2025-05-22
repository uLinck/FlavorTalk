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

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<Review> Reviews { get; set; } = [];
    public float? RatingAverage { get; private set; }

    public void Update(string name, string? description)
    {
        name.CannotBeNullOrEmpty();
        Name = name;
        Description = description;
    }
}
