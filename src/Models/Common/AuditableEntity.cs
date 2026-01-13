namespace E_Commerce.Models.Common;

public abstract class AuditableEntity : Entity
{
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset LastModifiedUtc { get; set; }
    
    protected AuditableEntity()
    { }

    protected AuditableEntity(Guid id)
        : base(id)
    {
    }
}
