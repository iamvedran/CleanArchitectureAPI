using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

// Unit represents a void type, since <see cref="System.Void"/> is not a valid return type in C#.
public class UpdateLeaveTypeCommand : IRequest<Unit> 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}