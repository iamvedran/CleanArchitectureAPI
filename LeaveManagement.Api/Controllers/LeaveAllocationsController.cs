using LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveAllocationsController>
    /// <summary>
    /// Get leave all allocations
    /// </summary>
    /// <returns>List of leave allocations</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /LeaveAllocations
    ///
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }

    // GET api/<LeaveAllocationsController>/5
    /// <summary>
    /// Get leave allocation details
    /// </summary>
    /// <returns>A single leave allocation</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /LeaveAllocations/5
    ///
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailQuery { Id = id });
        return Ok(leaveAllocation);
    }

    
    // POST api/<LeaveAllocationsController>
    /// <summary>
    /// Creates new leave type
    /// </summary>
    /// <returns>Created leave allocation id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /LeaveAllocations
    ///     {
    ///       "leaveTypeId": 1
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveAllocationsController>/5
    /// <summary>
    /// Updates leave allocations
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns>Updated leave type id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /LeaveAllocations/5
    ///     {
    ///       "id": 5,
    ///       "numberOfDays": 10,
    ///       "leaveTypeId": 1,
    ///       "period": 10
    ///     }
    ///
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationsController>/5
    /// <summary>
    /// Deletes leave type
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /LeaveAllocations/5
    ///
    /// </remarks>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}