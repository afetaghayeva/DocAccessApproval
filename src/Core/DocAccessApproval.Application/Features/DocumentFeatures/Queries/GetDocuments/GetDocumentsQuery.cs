using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Domain.SeedWork.Paging;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Queries.GetDocuments;

public class GetDocumentsQuery : IRequest<IPaginate<GetDocumentDto>>
{
    public GetDocumentsQuery(int index, int size)
    {
        Index = index;
        Size = size;
    }
    public int Index { get; init; }
    public int Size { get; init; }
}
