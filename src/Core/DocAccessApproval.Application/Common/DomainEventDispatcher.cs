using DocAccessApproval.Application.Interfaces;
using DocAccessApproval.Domain.SeedWork;
using MediatR;

namespace DocAccessApproval.Application.Common;

public class DomainEventDispatcher(IMediator mediator):IDomainEventDispatcher
{
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
