namespace PetFamily.SharedKernel;
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
