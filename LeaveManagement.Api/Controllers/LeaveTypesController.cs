using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get leave types
    /// </summary>
    /// <returns>List of leave types</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET /LeaveTypes
    ///
    /// </remarks>
    [HttpGet]
    public async Task<List<LeaveTypeDto>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    /// <summary>
    /// Get leave type details
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Leave type details</returns>
    /// <remarks>
    /// Sample request:
    ///
    /// GET /LeaveTypes/5
    ///
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));

        return Ok(leaveType);
    }

    // POST api/<LeaveTypesController>
    /// <summary>
    /// Creates new leave type
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns>Created leave type id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /LeaveTypes
    ///     {
    ///       "name": "Vacation",
    ///       "defaultDays": 100
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType)
    {
        var response = await _mediator.Send(leaveType);

        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveTypesController>/5
    /// <summary>
    /// Updates leave type
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns>Updated leave type id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /LeaveTypes/5
    ///     {
    ///       "id": "1",
    ///       "name": "Vacation",
    ///       "defaultDays": 100
    ///     }
    ///
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveType)
    {
        await _mediator.Send(leaveType);
        return NoContent();
    }

    // DELETE api/<LeaveTypesController>/5
    /// <summary>
    /// Deletes leave type
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Created leave type id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///
    /// </remarks>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveTypeCommand() { Id = id };
        await _mediator.Send(command);

        return NoContent();
    }
}