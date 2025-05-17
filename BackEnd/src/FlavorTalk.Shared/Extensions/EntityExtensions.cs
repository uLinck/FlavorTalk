namespace FlavorTalk.Shared.Extensions;
public static class EntityExtensions
{
    public static T With<T>(this T entity, Action<T> action) where T : class
    {
        action(entity);
        return entity;
    }
}
