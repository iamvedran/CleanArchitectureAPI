using MediatR;
using AutoMapper;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Models.Email;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using Microsoft.Extensions.Logging;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<CancelLeaveRequestCommand> _logger;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository leaveRequestRepository, 
        ILeaveTypeRepository leaveTypeRepository, 
        IMapper mapper, 
        IEmailSender emailSender, 
        ILogger<CancelLeaveRequestCommand> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }
            
        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                       $"has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ChangeLeaveRequestApprovalCommandHandler send mail error");
        }

        return Unit.Value;
    }
}