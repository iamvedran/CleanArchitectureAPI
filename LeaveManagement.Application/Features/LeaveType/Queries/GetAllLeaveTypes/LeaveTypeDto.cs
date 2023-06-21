namespace LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    /// <summary>
    /// Id
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
    /// <summary>
    /// Leave type name
    /// </summary>
    /// <example>Vacation</example>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Number of days
    /// </summary>
    /// <example>10</example>
    public int DefaultDays { get; set; }
}