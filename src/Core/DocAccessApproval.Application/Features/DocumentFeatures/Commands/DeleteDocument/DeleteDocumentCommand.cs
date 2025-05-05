using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.DeleteDocument;

public class DeleteDocumentCommand:IRequest<bool>
{
    public DeleteDocumentCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
}
