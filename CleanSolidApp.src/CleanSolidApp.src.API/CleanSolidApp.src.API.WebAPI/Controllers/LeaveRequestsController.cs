using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Commands;
using CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSolidApp.src.API.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public LeaveRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListItemDTO>>> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
        return Ok(leaveRequests);
    }

    [HttpGet("{leaveRequestID}")]
    public async Task<ActionResult<LeaveRequestDTO>> Get(int leaveRequestID)
    {
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest { ID = leaveRequestID });
        return Ok(leaveRequest);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CreateLeaveRequestDTO leaveRequest)
    {
        var command = new CreateLeaveRequestCommand { CreateLeaveRequestDTO = leaveRequest };
        var repsonse = await _mediator.Send(command);
        return Ok(repsonse);
    }

    [HttpPut("{leaveRequestID}")]
    public async Task<ActionResult> Put(int leaveRequestID, [FromBody]UpdateLeaveRequestDTO leaveRequest)
    {
        var command = new UpdateLeaveRequestCommand { LeaveRequestID = leaveRequestID, LeaveRequestDTO = leaveRequest };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("changeApproval/{leaveRequestID}")]
    public async Task<ActionResult> ChangeApproval(int leaveRequestID, [FromBody]ChangeLeaveRequestApprovalDTO leaveRequest)
    {
        var command = new UpdateLeaveRequestCommand { LeaveRequestID = leaveRequestID, ChangeLeaveRequestApprovalDTO = leaveRequest };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{leaveRequestID}")]
    public async Task<ActionResult> Delete(int leaveRequestID)
    {
        var command = new DeleteLeaveRequestCommand { LeaveRequestID = leaveRequestID };
        await _mediator.Send(command);
        return NoContent();
    }
}

