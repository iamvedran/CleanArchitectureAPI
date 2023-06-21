using LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    /// <summary>
    /// Comments
    /// </summary>
    /// <example>Lorem Ipsum</example>
    public string RequestComments { get; set; }
}