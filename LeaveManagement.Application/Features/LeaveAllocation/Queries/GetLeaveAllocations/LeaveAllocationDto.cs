using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class LeaveAllocationDto
{
    /// <summary>
    /// Id
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
    /// <summary>
    /// Number of days
    /// </summary>
    /// <example>10</example>
    public int NumberOfDays { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    /// <summary>
    /// Id of the leave type
    /// </summary>
    /// <example>1</example>
    public int LeaveTypeId { get; set; }
    /// <summary>
    /// Number
    /// </summary>
    /// <example>5</example>
    public int Period { get; set; }
}