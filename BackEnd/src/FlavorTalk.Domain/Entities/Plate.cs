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
    public string? Description { get; private set;}
    //TODO: Add Reviews
    public float? RatingAverage { get; }

    public void Update(string name, string? description)
    {
        name.CannotBeNullOrEmpty();
        Name = name;
        Description = description;
    }
}
