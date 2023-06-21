using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Domain.Models;
using Moq;
using Shouldly;

namespace LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;
    private readonly GetLeaveTypesQueryHandler _handler;

    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();
        _handler = new GetLeaveTypesQueryHandler(_mockMapper.Object, _mockLeaveTypeRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfLeaveTypes()
    {
        // Arrange
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType { Id = 1, DefaultDays = 10, Name = "Test Vacation" },
            new LeaveType { Id = 2, DefaultDays = 5, Name = "Test Sick" },
            new LeaveType { Id = 3, DefaultDays = 7, Name = "Test Leave" },
        };
        var expectedLeaveTypeDtos = new List<LeaveTypeDto>
        {
            new LeaveTypeDto { Id = 1, DefaultDays = 10, Name = "Test Vacation" },
            new LeaveTypeDto { Id = 2, DefaultDays = 5, Name = "Test Sick" },
            new LeaveTypeDto { Id = 3, DefaultDays = 7, Name = "Test Leave" },
        };

        _mockLeaveTypeRepository
            .Setup(r => r.GetAsync())
            .ReturnsAsync(leaveTypes);
        _mockMapper
            .Setup(m => m.Map<List<LeaveTypeDto>>(leaveTypes))
            .Returns(expectedLeaveTypeDtos);

        // Act
        var result = await _handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        // Assert
        result.Count.ShouldBe(3);
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.ShouldBeEquivalentTo(expectedLeaveTypeDtos);
    }
}