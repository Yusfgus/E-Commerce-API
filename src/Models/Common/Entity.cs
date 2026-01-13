namespace E_Commerce.Models.Common;

public abstract class Entity
{
    public Guid Id { get; set; }

    protected Entity()
    { }

    protected Entity(Guid id)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
    }
}