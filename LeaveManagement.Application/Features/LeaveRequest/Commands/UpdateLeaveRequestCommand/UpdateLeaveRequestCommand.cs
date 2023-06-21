using MediatR;
using LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;

public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public int Id { get; set; }
    public string RequestComments { get; set; } = string.Empty;
    public bool Cancelled { get; set; }
}