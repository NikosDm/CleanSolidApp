using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CleanSolidApp.src.Core.Application.Features.LeaveTypes.Requests.Commands;

public class DeleteLeaveTypeCommand : IRequest
{
    public int LeaveTypeID { get; set; }
}