namespace FlavorTalk.Shared.Extensions;
public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        items.CannotBeNull("items");
        action.CannotBeNull("action");
        foreach (var item in items)
        {
            action(item);
        }
    }

}
