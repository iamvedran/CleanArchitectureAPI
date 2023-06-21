using FluentValidation;
using LeaveManagement.Application.Contracts.Persistence;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidatior : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidatior(ILeaveTypeRepository leaveTypeRepository)
    {
        // property checks
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50).WithMessage("{Property} must be less than 50 characters");

        RuleFor(p => p.DefaultDays)
            .GreaterThan(0).WithMessage("{PropertyName} cannot be less than 1")
            .LessThan(100).WithMessage("{PropertyName} must be less than 100");
            
        // repo checks
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(d => d)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}