using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.CreateAccessRequest;

public class CreateAccessRequestCommandHandler(IDocumentRepository documentRepository, IMapper mapper)
    : IRequestHandler<CreateAccessRequestCommand, AccessRequestDto>
{
    public async Task<AccessRequestDto> Handle(CreateAccessRequestCommand request, CancellationToken cancellationToken)
    {
        var combinedAccess = GetCombinedAccess(request);

        var document = await documentRepository.GetAsync(d => d.Id == request.DocumentId);
        DocumentException.ThrowIfNull(document);

        var accessRequest=document.AddAccessRequest(request.UserId, combinedAccess, request.Reason, request.ExpireDate);
        await documentRepository.UnitOfWork.SaveChangesAsync();

        var result = mapper.Map<AccessRequestDto>(accessRequest);
        return result;
    }

    private static int GetCombinedAccess(CreateAccessRequestCommand request)
    {
        if (request.AccessTypes == null || request.AccessTypes.Count == 0)
            throw new DocumentException("Access types cannot be null or empty.");

        int bitwiseAnd = request.AccessTypes
        .Cast<int>()
        .Aggregate((a, b) => a | b); 

        return bitwiseAnd;
    }
}
