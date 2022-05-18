namespace SS.DataAccess.Entities.Base;

public abstract class EntityBase
{
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime? RecCreated { get; set; }

    public bool? IsRemoved { get; set; }

    public DateTime? RecModified { get; set; }
}