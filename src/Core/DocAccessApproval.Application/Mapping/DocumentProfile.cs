using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.SeedWork.Paging;

namespace DocAccessApproval.Application.Mapping;

public class DocumentProfile : Profile
{
    public DocumentProfile()
    {
        CreateMap<Document, DocumentDto>();
        CreateMap<IPaginate<Document>, Paginate<DocumentDto>>();

        CreateMap<Document, GetDocumentDto>();
        CreateMap<IPaginate<Document>, Paginate<GetDocumentDto>>();

        CreateMap<AccessRequest, AccessRequestDto>();
        CreateMap<IPaginate<AccessRequest>, Paginate<AccessRequestDto>>();

        CreateMap<Decision, DecisionDto>();
    }
}
