using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;

public class LeaveRequestListItemDTO : BaseDTO
{
    public LeaveTypeDTO LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public bool Approved { get; set; }
}
