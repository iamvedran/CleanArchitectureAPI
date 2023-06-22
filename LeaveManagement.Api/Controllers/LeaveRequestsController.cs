using LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveRequestsController>
    /// <summary>
    /// Get leave all leave requests
    /// </summary>
    /// <returns>List of leave allocations</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /LeaveRequests
    ///
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
        return Ok(leaveRequests);
    }

    // GET api/<LeaveRequestsController>/5
    /// <summary>
    /// Get leave leave reqest details
    /// </summary>
    /// <returns>A single leave allocation</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /LeaveRequests/5
    ///
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
        return Ok(leaveRequest);
    }

    // POST api/<LeaveRequestsController>
    /// <summary>
    /// Creates new leave request
    /// </summary>
    /// <returns>Created leave allocation id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /LeaveRequests
    ///     {
    ///       "startDate": "2023-01-15",
    ///       "endDate": "2023-01-1",
    ///       "leaveTypeId": 1,
    ///       "requestComments": "Lorem Ipsum"
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
    {
        var response = await _mediator.Send(leaveRequest);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveRequestsController>/5
    /// <summary>
    /// Updates a leave request
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns>Updated leave type id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /LeaveRequests/5
    ///     {
    ///       "startDate": "2023-01-15",
    ///       "endDate": "2023-01-1",
    ///       "leaveTypeId": 1,
    ///       "id": 0,
    ///       "requestComments": "string",
    ///       "cancelled": true
    ///     }
    ///
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
    {
        await _mediator.Send(leaveRequest);
        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/CancelRequest/
    /// <summary>
    /// Cancels leave request
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /LeaveRequests/CancelRequest
    ///     {
    ///       "id": 1
    ///     }
    ///
    /// </remarks>
    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        await _mediator.Send(cancelLeaveRequest);
        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/UpdateApproval/
    // PUT api/<LeaveRequestsController>/CancelRequest/
    /// <summary>
    /// Updates leave request approval status
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /LeaveRequests/UpdateApproval
    ///     {
    ///       "id": 1
    ///     }
    ///
    /// </remarks>
    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
    {
        await _mediator.Send(updateApprovalRequest);
        return NoContent();
    }

    // DELETE api/<LeaveRequestsController>/5
    /// <summary>
    /// Deletes leave request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /LeaveRequests/5
    ///
    /// </remarks>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveRequestCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}