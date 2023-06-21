using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand : IRequest<Unit>
{
    /// <summary>
    /// Id of the leave request to approve
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
    /// <summary>
    /// Is approved?
    /// </summary>
    /// <example>true</example>
    public bool Approved { get; set; }
}