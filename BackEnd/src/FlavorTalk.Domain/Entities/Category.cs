using FlavorTalk.Shared.Extensions;

namespace FlavorTalk.Domain.Entities;
public class Category : BaseEntity
{
    private Category() { }
    public Category(string name, List<Plate> plates)
    {
        name.CannotBeNull();
        plates.CannotBeNullOrEmpty();

        Name = name;
        Plates = plates;
    }

    public string Name { get; private set; }
    public List<Plate> Plates { get; private set; }

    public void AddPlate(Plate plate) => Plates.Add(plate);
    public void Update(string name, List<Plate> plates)
    {
        name.CannotBeNullOrEmpty();
        plates.CannotBeNullOrEmpty();

        Name = name;
        Plates = plates;
    }
}
