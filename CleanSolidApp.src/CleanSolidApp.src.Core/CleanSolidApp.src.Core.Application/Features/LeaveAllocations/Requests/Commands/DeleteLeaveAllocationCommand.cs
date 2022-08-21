using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Commands;

public class DeleteLeaveAllocationCommand : IRequest
{
    public int LeaveAllocationID { get; set; }
}
