using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using Moq;
using Shouldly;

namespace LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;
    private readonly CreateLeaveTypeCommandHandler _handler;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();
        _handler = new CreateLeaveTypeCommandHandler(_mockMapper.Object, _mockLeaveTypeRepository.Object);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldCreateLeaveTypeAndReturnId()
    {
        // Arrange
        var command = new CreateLeaveTypeCommand
        {
            Name = "Test Vacation",
            DefaultDays = 10
        };
        var leaveTypeToCreate = new Domain.Models.LeaveType
        {
            Id = 1,
            Name = "Test Vacation",
            DefaultDays = 10
        };

        var createdLeaveType = new Domain.Models.LeaveType { Id = 1 };

        _mockMapper.Setup(m => m.Map<Domain.Models.LeaveType>(command))
            .Returns(leaveTypeToCreate);
        _mockLeaveTypeRepository.Setup(r => r.CreateAsync(leaveTypeToCreate))
            .ReturnsAsync(createdLeaveType);
        _mockLeaveTypeRepository.Setup(r => r.IsLeaveTypeUnique(command.Name))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(leaveTypeToCreate.Id);
        _mockLeaveTypeRepository.Verify(r => r.CreateAsync(leaveTypeToCreate), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidData_ShouldThrowBadRequestException()
    {
        // Arrange
        var command = new CreateLeaveTypeCommand();
        _mockLeaveTypeRepository.Setup(r => r.CreateAsync(It.IsAny<Domain.Models.LeaveType>()));

        // Act
        var exception =
            await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.ShouldBe("Validation errors for LeaveType");
        _mockLeaveTypeRepository.Verify(r => r.CreateAsync(It.IsAny<Domain.Models.LeaveType>()), Times.Never);
    }
}