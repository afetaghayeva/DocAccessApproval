namespace DocAccessApproval.Application.Features.DocumentFeatures.Dtos;

public class DecisionDto
{
    public Guid Id { get; set; }
    public Guid AccessRequestId { get; set; }
    public Guid UserId { get; set; }
    public bool IsApproved { get; set; }
    public string? Comment { get; set; }
}
