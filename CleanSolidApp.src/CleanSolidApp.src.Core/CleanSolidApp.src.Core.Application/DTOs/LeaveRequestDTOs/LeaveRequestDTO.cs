using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanSolidApp.src.Core.Application.DTOs.Common;
using CleanSolidApp.src.Core.Application.DTOs.LeaveTypeDTOs;
using CleanSolidApp.src.Core.Application.Models.Identity;

namespace CleanSolidApp.src.Core.Application.DTOs.LeaveRequestDTOs;

public class LeaveRequestDTO : BaseDTO, ILeaveRequestDTO
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveTypeDTO LeaveType { get; set; }
    public int LeaveTypeID { get; set; }
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; }
    public DateTime DateActioned { get; set; }
    public bool Approved { get; set; }
    public bool Cancelled { get; set; }
    public Employee Employee { get; set; }
    public string RequestingEmployeeID { get; set; }
}
