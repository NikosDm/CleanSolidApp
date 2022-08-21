using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveAllocationDTOs;

public interface ILeaveAllocationDTO
{
    public int NumberOfDays { get; set; }
    public int LeaveTypeID { get; set; }
    public int Period { get; set; }
}
