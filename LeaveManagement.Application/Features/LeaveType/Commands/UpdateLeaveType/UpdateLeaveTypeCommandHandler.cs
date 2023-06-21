using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // validate data


        // convert to domain entity object
        var leaveTypeToUpdate = _mapper.Map<Domain.Models.LeaveType>(request);

        // add to database
        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // return value
        return  Unit.Value;

    }
}