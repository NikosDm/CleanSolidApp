using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public UpdateLeaveAllocationDTO UpdateLeaveAllocationDTO { get; set; }
}
