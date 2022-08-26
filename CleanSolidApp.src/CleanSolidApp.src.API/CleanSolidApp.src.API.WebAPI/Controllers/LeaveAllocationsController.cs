using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;
using CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSolidApp.src.API.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveAllocationDTO>>> Get()
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
        return leaveAllocations;
    }

    [HttpGet("{leaveAllocationID}")]
    public async Task<ActionResult<LeaveAllocationDTO>> Get(int leaveAllocationID)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest { ID = leaveAllocationID });
        return leaveAllocation;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CreateLeaveAllocationDTO leaveAllocationDTO)
    {
        var command = new CreateLeaveAllocationCommand { CreateLeaveAllocationDTO = leaveAllocationDTO };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody]UpdateLeaveAllocationDTO leaveAllocationDTO)
    {
        var command = new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDTO = leaveAllocationDTO };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{leaveAllocationID}")]
    public async Task<ActionResult> Delete(int leaveAllocationID)
    {
        var command = new DeleteLeaveAllocationCommand { LeaveAllocationID = leaveAllocationID };
        await _mediator.Send(command);
        return NoContent();
    }
}
