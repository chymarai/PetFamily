using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared;
public abstract class SoftDeletableEntity<TId> : Entity<TId>
{
    protected SoftDeletableEntity(TId id) : base(id)
    {
    }

    public virtual void Delete()
    {
        if (IsDeleted) return;

        IsDeleted = true;
        //DeletionTime = DateTime.UtcNow;
    }

    public virtual void Restore()
    {
        if (!IsDeleted) return;

        IsDeleted = false;
        //DeletionTime = null;
    }

    public bool IsDeleted { get; private set; }
    //public DateTime? DeletionTime { get; private set; }
}
