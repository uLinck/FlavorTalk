using FlavorTalk.Shared.Extensions;

namespace FlavorTalk.Domain.Entities;
public class Catalog : BaseEntity
{
    private Catalog() { }
    public Catalog(List<Category> categories)
    {
        categories.CannotBeNullOrEmpty();
        Categories = categories;
    }

    public List<Category> Categories { get; set; }
}
