using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestListRequest: IRequest<List<LeaveRequestListItemDTO>>
{
    
}
