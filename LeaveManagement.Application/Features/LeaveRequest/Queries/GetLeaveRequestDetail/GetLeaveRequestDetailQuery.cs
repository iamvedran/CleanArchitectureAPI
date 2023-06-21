using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailsDto>
{
    public int Id { get; set; }
}