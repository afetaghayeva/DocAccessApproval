namespace DocAccessApproval.Domain.SeedWork;

public interface IRepository<T> where T : BaseEntity, new()
{
    IUnitOfWork UnitOfWork { get; }
}
