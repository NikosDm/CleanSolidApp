using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;

public class LeaveTypeDTO : BaseDTO, ILeaveTypeDTO
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}
