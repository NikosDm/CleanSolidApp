using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;

public class ChangeLeaveRequestApprovalDTO : BaseDTO
{
    public bool? Approved { get; set; }
}
