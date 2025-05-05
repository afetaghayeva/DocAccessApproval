using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Domain.AggregateModels.UserAggregate;

public class Role : BaseEntity
{

    public Role()
    {

    }
    public Role(Guid id) : this()
    {
        Id = id;
    }
    public Role(Guid id, string name) : this(id)
    {
        Name = name;
    }
    public Role(string name) : this(Guid.NewGuid(), name)
    {

    }
    public string Name { get; private set; }
}
