using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}