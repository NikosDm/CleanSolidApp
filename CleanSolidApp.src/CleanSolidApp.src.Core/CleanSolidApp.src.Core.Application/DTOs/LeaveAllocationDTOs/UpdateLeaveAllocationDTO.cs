using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;

public class UpdateLeaveAllocationDTO : BaseDTO, ILeaveAllocationDTO 
{
    public int NumberOfDays { get; set; }
    public int LeaveTypeID { get; set; }
    public int Period { get; set; }
}
