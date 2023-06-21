using MediatR;
using AutoMapper;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Models.Email;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using Microsoft.Extensions.Logging;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<CancelLeaveRequestCommand> _logger;

    public UpdateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository, 
        ILeaveTypeRepository leaveTypeRepository, 
        IMapper mapper,
        IEmailSender emailSender, ILogger<CancelLeaveRequestCommand> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);


        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            // send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                       $"has been updated successfully.",
                Subject = "Leave Request Updated"
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