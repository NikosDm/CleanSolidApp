using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;

public class LeaveAllocationDTO : BaseDTO
{
    public int NumberOfDays { get; set; }
    public LeaveTypeDTO LeaveType { get; set; }
    public int LeaveTypeID { get; set; }
    public int Period { get; set; }
}