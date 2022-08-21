using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDTO>
{
    public int ID { get; set; }
}
