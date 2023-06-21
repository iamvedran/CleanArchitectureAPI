using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>
{
}