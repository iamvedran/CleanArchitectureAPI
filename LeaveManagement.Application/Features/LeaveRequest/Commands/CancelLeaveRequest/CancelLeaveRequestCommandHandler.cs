using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Models.Email;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<CancelLeaveRequestCommand> _logger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender, ILogger<CancelLeaveRequestCommand> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
    }
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }
                
        leaveRequest.Cancelled = true;

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                       $"has been cancelled successfully.",
                Subject = "Leave Request Cancelled"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CancelLeaveRequestCommandHandler send mail error");
        }
        return Unit.Value;
    }
}