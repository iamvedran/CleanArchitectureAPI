using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    /// <summary>
    /// Id of the leave type
    /// </summary>
    /// <example>1</example>
    public int LeaveTypeId { get; set; }
}