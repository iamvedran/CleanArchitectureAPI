using LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // get entity object
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

        // verify record exists
        if (leaveTypeToDelete == null)
        {
            return Unit.Value;
        }

        // remove from
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        // retun record id
        return Unit.Value;
    }
}