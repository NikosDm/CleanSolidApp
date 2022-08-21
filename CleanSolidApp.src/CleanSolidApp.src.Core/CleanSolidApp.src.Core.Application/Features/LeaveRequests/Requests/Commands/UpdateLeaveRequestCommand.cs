using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<Unit>
{
    public int LeaveRequestID { get; set; }
    public UpdateLeaveRequestDTO LeaveRequestDTO { get; set; }
    public ChangeLeaveRequestApprovalDTO ChangeLeaveRequestApprovalDTO { get; set; }
}
