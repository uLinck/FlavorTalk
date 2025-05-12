using FlavorTalk.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlavorTalk.Infrastructure.Data;

public class FlavorTalkContext : IdentityDbContext<User, Role, Guid>
{
    public FlavorTalkContext(DbContextOptions options) : base(options)
    {
    }

    protected FlavorTalkContext()
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Override default AspNet Identity table names
        modelBuilder.Entity<User>(entity => entity.ToTable(name: "Users"));
        modelBuilder.Entity<Role>(entity => entity.ToTable(name: "Roles"));
        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));
        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));
        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));
        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));
    }
}
