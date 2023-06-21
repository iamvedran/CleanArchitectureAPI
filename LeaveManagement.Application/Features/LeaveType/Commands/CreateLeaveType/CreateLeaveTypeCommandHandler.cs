using AutoMapper;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // validate data
        var validator = new CreateLeaveTypeCommandValidatior(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Validation errors for LeaveType", validationResult);
        }

        // convert to domain entity type object
        var leaveTypeToCreate = _mapper.Map<Domain.Models.LeaveType>(request);

        // add to database
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        // return record id
        return leaveTypeToCreate.Id;
    }
}