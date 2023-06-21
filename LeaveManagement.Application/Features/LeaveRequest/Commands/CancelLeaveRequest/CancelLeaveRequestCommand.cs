using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest<Unit>
{
    /// <summary>
    /// Id of the leave request to cancel
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
}