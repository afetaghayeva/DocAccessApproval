using AutoMapper;
using DocAccessApproval.Application.Features.DocumentFeatures.Commands.CreateAccessRequest;
using DocAccessApproval.Application.Features.DocumentFeatures.Dtos;
using DocAccessApproval.Application.Interfaces.Repositories;
using DocAccessApproval.Domain.AggregateModels.DocumentAggregate;
using DocAccessApproval.Domain.SeedWork;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace DocAccessApproval.Tests.Application.Handlers;

public class CreateAccessRequestCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Add_AccessRequest_And_Return_Dto()
    {
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var reason = "Need temporary access";
        var expireDate = DateTime.UtcNow.AddDays(1);
        var accessTypes = new List<AccessType> { AccessType.Read, AccessType.Edit };

        var command = new CreateAccessRequestCommand(documentId, reason, expireDate, accessTypes);
        command.UserId = userId;

        var document = new Document(documentId);

        var docRepoMock = new Mock<IDocumentRepository>();
        docRepoMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Document, bool>>>(), false, null))
            .ReturnsAsync(document);
        docRepoMock
            .Setup(repo => repo.UnitOfWork.SaveChangesAsync(default))
            .Returns(Task.FromResult(1));

        var mapperMock = new Mock<IMapper>();
        var expectedDto = new AccessRequestDto();
        mapperMock
            .Setup(m => m.Map<AccessRequestDto>(It.IsAny<AccessRequest>()))
            .Returns(expectedDto);

        var handler = new CreateAccessRequestCommandHandler(docRepoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedDto, result);
        docRepoMock.Verify(repo => repo.UnitOfWork.SaveChangesAsync(default), Times.Once);
        mapperMock.Verify(m => m.Map<AccessRequestDto>(It.IsAny<AccessRequest>()), Times.Once);
    }
}
