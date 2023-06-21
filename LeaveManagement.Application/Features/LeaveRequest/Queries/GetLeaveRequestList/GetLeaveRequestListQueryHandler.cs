using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestWithDetails();
        var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        return requests;
    }
}