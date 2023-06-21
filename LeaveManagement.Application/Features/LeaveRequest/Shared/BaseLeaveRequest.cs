namespace LeaveManagement.Application.Features.LeaveRequest.Shared;

public abstract class BaseLeaveRequest
{
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-15</example>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-1</example>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Id of the leave type
    /// </summary>
    /// <example>1</example>
    public int LeaveTypeId { get; set; }
}