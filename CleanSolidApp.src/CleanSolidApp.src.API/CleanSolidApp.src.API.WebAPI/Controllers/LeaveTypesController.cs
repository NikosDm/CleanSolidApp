using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Commands;
using CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanSolidApp.src.API.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveTypeDTO>>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
        return leaveTypes;
    }

    [HttpGet("{leaveTypeID}")]
    public async Task<ActionResult<LeaveTypeDTO>> Get(int leaveTypeID)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { ID = leaveTypeID });
        return leaveType;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDTO leavetype)
    {
        var command = new CreateLeaveTypeCommand { LeaveTypeDTO = leavetype };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody]LeaveTypeDTO leavetype)
    {
        var command = new UpdateLeaveTypeCommand { LeaveTypeDTO = leavetype };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{leaveTypeID}")]
    public async Task<ActionResult> Delete(int leaveTypeID)
    {
        var command = new DeleteLeaveTypeCommand { LeaveTypeID = leaveTypeID };
        await _mediator.Send(command);
        return NoContent();
    }
}