namespace LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class LeaveTypeDetailsDto
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
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-15</example>
    public DateTime? DateCreated { get; set; }
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-16</example>
    public DateTime? DateModified { get; set; }
}

