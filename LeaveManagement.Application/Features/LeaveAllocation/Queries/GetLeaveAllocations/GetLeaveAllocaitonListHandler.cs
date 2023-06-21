using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocaitonListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocaitonListHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetails();
        var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return allocations;
    }
}