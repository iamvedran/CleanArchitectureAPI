using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class LeaveRequestDetailsDto
{
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-12</example>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-15</example>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Employee identifier
    /// </summary>
    /// <example>Pero</example>
    public string RequestingEmployeeId { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    /// <summary>
    /// Id of the leave type
    /// </summary>
    /// <example>1</example>
    public int LeaveTypeId { get; set; }
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-01</example>
    public DateTime DateRequested { get; set; }
    /// <summary>
    /// Comments
    /// </summary>
    /// <example>Lorem Ipsum</example>
    public string RequestComments { get; set; }
    /// <summary>
    /// Date
    /// </summary>
    /// <example>2023-01-01</example>
    public DateTime? DateActioned { get; set; }
    /// <summary>
    /// Is approved?
    /// </summary>
    /// <example>true</example>
    public bool? Approved { get; set; }
    /// <summary>
    /// Is cancelled?
    /// </summary>
    /// <example>false</example>
    public bool Cancelled { get; set; }
}