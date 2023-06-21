using FluentValidation;
using LeaveManagement.Application.Contracts.Persistence;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidatior : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandValidatior(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} must exist.");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType != null;
    }
}