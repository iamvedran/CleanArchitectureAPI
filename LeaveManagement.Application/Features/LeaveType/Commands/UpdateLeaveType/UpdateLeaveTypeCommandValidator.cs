using FluentValidation;
using LeaveManagement.Application.Contracts.Persistence;

namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        
        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveTypeMustExist);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters");

            RuleFor(p => p.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            // repo checks
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(d => d)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exists");

            RuleFor(d => d.Id)
                .MustAsync(LeaveTypeMustExist);
        }

        private  async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

            return leaveType != null;
        }
    }
}
