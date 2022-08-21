using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;

public interface ILeaveRequestDTO
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeID { get; set; }
    public string RequestComments { get; set; }
}
