using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Domain;
public class User : IdentityUser<Guid> 
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
