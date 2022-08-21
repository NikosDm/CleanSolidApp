using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveRequests.Requests.Commands;

public class DeleteLeaveRequestCommand : IRequest
{
    public int LeaveRequestID { get; set; }
}
