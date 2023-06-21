using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _maper;

    public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper maper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _maper = maper;
    }
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        return _maper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}