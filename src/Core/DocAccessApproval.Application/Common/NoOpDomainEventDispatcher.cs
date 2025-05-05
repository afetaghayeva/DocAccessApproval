using DocAccessApproval.Application.Interfaces;
using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Application.Common;

public class NoOpDomainEventDispatcher:IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
