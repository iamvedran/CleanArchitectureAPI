using LeaveManagement.Domain.Models.Common;

namespace LeaveManagement.Domain.Models;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}