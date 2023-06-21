using AutoMapper;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using LeaveManagement.Application.Models.Email;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILogger<CancelLeaveRequestCommand> _logger;

    public CreateLeaveRequestCommandHandler(
        IEmailSender emailSender,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveRequestRepository leaveRequestRepository,
        ILogger<CancelLeaveRequestCommand> logger)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }
            
        // Create leave request
        var leaveRequest = _mapper.Map<Domain.Models.LeaveRequest>(request);
        await _leaveRequestRepository.CreateAsync(leaveRequest);

        try
        {
            // send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                       $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
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