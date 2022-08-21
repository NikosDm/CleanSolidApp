using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;

public class UpdateLeaveRequestDTO: BaseDTO, ILeaveRequestDTO
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeID { get; set; }
    public string RequestComments { get; set; }
    public bool Cancelled { get; set; }
}
