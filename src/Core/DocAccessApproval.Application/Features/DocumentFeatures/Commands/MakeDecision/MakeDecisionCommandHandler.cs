using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using MediatR;

namespace DocAccessApproval.Application.Features.DocumentFeatures.Commands.MakeDecision;

public class MakeDecisionCommandHandler(IDocumentRepository documentRepository,IMapper mapper) : IRequestHandler<MakeDecisionCommand, DecisionDto>
{
    public async Task<DecisionDto> Handle(MakeDecisionCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetDocByAccessRequestIdAsync(request.AccessRequestId);
        DocumentException.ThrowIfNull(document);

        var decision=document.SetDecision(request.AccessRequestId, request.UserId, request.IsApproved, request.Comment);
        await documentRepository.UnitOfWork.SaveChangesAsync();

        var result = mapper.Map<DecisionDto>(decision);
        return result;
    }
}
