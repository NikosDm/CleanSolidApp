using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDTO>>
{
    
}
