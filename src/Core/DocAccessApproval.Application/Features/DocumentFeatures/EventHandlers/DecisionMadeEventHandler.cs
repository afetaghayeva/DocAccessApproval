using DocAccessApproval.Domain.AggregateModels.DocumentAggregate.Events;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.EventHandlers;

public class DecisionMadeEventHandler : INotificationHandler<DecisionMadeEvent>
{
    public Task Handle(DecisionMadeEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Access Request {notification.AccessRequestId} {notification.RequestStatus} by user {notification.ApprovedByUserId}");

        // TODO: Call Email/SMS/Push notification service here

        return Task.CompletedTask;
    }
}
