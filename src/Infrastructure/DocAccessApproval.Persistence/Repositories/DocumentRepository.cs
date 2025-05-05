using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.SeedWork.Paging;
using DocAccessApproval.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DocAccessApproval.Persistence.Repositories;

public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(DocAccessApprovalDbContext context) : base(context)
    {
    }

    public async Task<IPaginate<AccessRequest>> GetAccessRequestsByUserIdAsync(Guid userId, int index, int size, RequestStatus? requestStatus)
    {
        var accessRequests = await base._context.AccessRequests
            .Where(ar => ar.UserId == userId && (requestStatus == null || ar.RequestStatus == requestStatus))
            .ToPaginateAsync(index, size);

        return accessRequests;
    }

    public async Task<IPaginate<AccessRequest>> GetAllAccessRequestsAsync(int index, int size, RequestStatus requestStatus = RequestStatus.Pending)
    {
        var accessRequests = await base._context.AccessRequests
            .Where(ar => ar.RequestStatus == requestStatus)
            .ToPaginateAsync(index, size);
        return accessRequests;
    }

    public async Task<Document> GetDocByAccessRequestIdAsync(Guid accessRequestId)
    {
        var document = await base._context.Documents.Include(d => d.AccessRequests)
            .FirstOrDefaultAsync(d => d.AccessRequests.Any(a => a.Id == accessRequestId));
        return document;
    }
}
