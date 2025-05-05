﻿using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Application.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
