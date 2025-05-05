using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetDocumentById;

public class GetDocumentByIdQuery:IRequest<DocumentDto>
{
    public GetDocumentByIdQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

}
