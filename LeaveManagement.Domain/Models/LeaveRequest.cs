using LeaveManagement.Domain.Models.Common;

namespace LeaveManagement.Domain.Models;

public class LeaveRequest : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestedComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestindEmployeeId { get; set; } = string.Empty;
}