using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandHandler(IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidatior(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
        }

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);


        var leaveAllocation = _mapper.Map<Domain.Models.LeaveAllocation>(request);
        await _leaveAllocationRepository.CreateAsync(leaveAllocation);

        return leaveAllocation.Id;
    }
}