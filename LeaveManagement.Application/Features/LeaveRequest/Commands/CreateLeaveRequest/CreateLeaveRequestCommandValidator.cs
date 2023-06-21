using FluentValidation;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
    }
}