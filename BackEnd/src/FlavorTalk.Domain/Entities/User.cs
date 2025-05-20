using Microsoft.AspNetCore.Identity;

namespace FlavorTalk.Domain.Entities;
public class User : UserBaseEntity
{
    public string Name { get; set; }

    #pragma warning disable CS8618
    protected User() { }

    public User(string name, string email)
    {
        Name = name;
        UserName = email;
        Email = email;
    }
}

public class Role : IdentityRole<Guid>
{
    protected Role() { }
}
