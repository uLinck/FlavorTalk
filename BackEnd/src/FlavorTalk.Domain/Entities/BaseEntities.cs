using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Domain.Entities;

public interface ISoftDeletable
{
    DateTime? DeletedAtUtc { get; }
    bool IsDeleted { get; }
    User? DeletedBy { get; }
    void Delete(User? deletedBy = null);
}

public abstract class BaseEntity : ISoftDeletable
{
    public Guid Id { get; protected set; }

    public DateTime? DeletedAtUtc { get; protected set; }

    public bool IsDeleted => DeletedAtUtc.HasValue;

    public User? DeletedBy { get; protected set; }

    public virtual void Delete(User? deletedBy = null)
    {
        if (IsDeleted)
            return;

        DeletedAtUtc = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }
}

public abstract class UserBaseEntity : IdentityUser<Guid>, ISoftDeletable
{
    public DateTime? DeletedAtUtc { get; protected set; }

    public bool IsDeleted => DeletedAtUtc.HasValue;

    public User? DeletedBy { get; protected set; }

    public virtual void Delete(User? deletedBy = null)
    {
        if (IsDeleted)
            return;

        DeletedAtUtc = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }
}
