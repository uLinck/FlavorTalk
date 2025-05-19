using FlavorTalk.Domain.Resources;
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

    public List<Category> Categories { get; private set; }

    public void AddCategory(Category category)
    {
        if (Categories.Any(c => string.Equals(c.Name, category.Name, StringComparison.InvariantCultureIgnoreCase)))
            throw new ArgumentException(Errors.CategoriesMusHaveUniqueName);

        Categories.Add(category);
    }
}
