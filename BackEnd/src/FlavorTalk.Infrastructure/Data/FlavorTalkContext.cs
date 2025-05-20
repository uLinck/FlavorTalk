using FlavorTalk.Domain.Entities;
using FlavorTalk.Shared.Extensions;
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

    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Plate> Plates { get; set; }
    public DbSet<Merchant> Merchants { get; set; }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Review Config
        modelBuilder.Entity<Review>()
            .HasKey(r => new { r.Id, r.AuthorId });

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Author)
            .WithMany()
            .HasForeignKey(r => r.AuthorId)
            .IsRequired();
        #endregion

        #region Override default AspNet Identity table names
        modelBuilder.Entity<User>(entity => entity.ToTable(name: "Users"));
        modelBuilder.Entity<Role>(entity => entity.ToTable(name: "Roles"));

        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));
        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));
        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));
        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));
        #endregion

        modelBuilder.Model.GetEntityTypes()
            .Select(t => t.ClrType)
            .Where(t => t.IsSubclassOf(typeof(BaseEntity)))
            .ForEach(sde =>
            {
                modelBuilder.Entity(sde)
                    .HasKey(nameof(BaseEntity.Id));
            });
    }


}
