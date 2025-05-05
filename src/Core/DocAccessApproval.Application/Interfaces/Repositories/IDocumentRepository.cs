using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.SeedWork.Paging;

namespace DocAccessApproval.Application.Interfaces.Repositories;

public interface IDocumentRepository : IGenericRepository<Document>
{
    Task<Document> GetDocByAccessRequestIdAsync(Guid accessRequestId);
    Task<IPaginate<AccessRequest>> GetAccessRequestsByUserIdAsync(Guid userId, int index, int size, RequestStatus? requestStatus);
    Task<IPaginate<AccessRequest>> GetAllAccessRequestsAsync(int index, int size, RequestStatus requestStatus = RequestStatus.Pending);
}
