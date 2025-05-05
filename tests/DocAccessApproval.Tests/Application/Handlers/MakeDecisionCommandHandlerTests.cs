using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.MakeDecision;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using FluentAssertions.Execution;
using Moq;

namespace DocAccessApproval.Tests.Application.Handlers;

public class MakeDecisionCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ApproveOrReject_AccessRequest()
    {
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var isApproved = true;
        var comment = "accepted";

        var reason = "Need temporary access";
        var expireDate = DateTime.UtcNow.AddDays(1);


        var document = new Document(documentId);
        var expectedAccessRequest = document.AddAccessRequest(userId, 1, reason, expireDate);

        var command = new MakeDecisionCommand(expectedAccessRequest.Id, userId, isApproved, comment);


        var docRepoMock = new Mock<IDocumentRepository>();
        docRepoMock
            .Setup(repo => repo.GetDocByAccessRequestIdAsync(expectedAccessRequest.Id))
            .ReturnsAsync(document);
        docRepoMock
            .Setup(repo => repo.UnitOfWork.SaveChangesAsync(default))
            .Returns(Task.FromResult(1));

        var mapperMock = new Mock<IMapper>();
        var expectedDto = new DecisionDto();
        mapperMock
            .Setup(m => m.Map<DecisionDto>(It.IsAny<Decision>()))
            .Returns(expectedDto);

        var handler = new MakeDecisionCommandHandler(docRepoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedDto, result);
        docRepoMock.Verify(repo => repo.UnitOfWork.SaveChangesAsync(default), Times.Once);
        mapperMock.Verify(m => m.Map<DecisionDto>(It.IsAny<Decision>()), Times.Once);

    }
}
