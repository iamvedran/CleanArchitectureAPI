using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<int>
{
    /// <summary>
    /// Name of the leave type
    /// </summary>
    /// <example>1</example>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Number of days
    /// </summary>
    /// <example>1</example>
    public int DefaultDays { get; set; }
}