using LeaveManagement.Domain.Models;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace LeavemManagement.Persistence.IntegrationTests;

public class LeaveDatabaseContextTests : IDisposable
{
    private readonly LeaveDatabaseContext _dbContext;

    public LeaveDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<LeaveDatabaseContext>()
            .UseInMemoryDatabase("DummyDatabase")
            .Options;

        _dbContext = new LeaveDatabaseContext(dbOptions);
    }

    [Fact]
    public async void GivenLeaveTypeWhenSavingSaveSetDateCreatedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        // Act
        await _dbContext.LeaveTypes.AddAsync(leaveType);
        await _dbContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void GivenLeaveTypeWhenSavingSaveSetDateModifiedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 2,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        // Act
        await _dbContext.LeaveTypes.AddAsync(leaveType);
        await _dbContext.SaveChangesAsync();

        // Assert
        leaveType.DateModified.ShouldNotBeNull();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}